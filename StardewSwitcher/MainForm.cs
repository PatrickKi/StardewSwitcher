using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml;

namespace StardewSwitcher
{
  public partial class MainForm : Form
  {
    private string _savesDirectory = string.Empty;
    public string SavesDirectory
    {
      get => _savesDirectory;
      set
      {
        _savesDirectory = string.Empty;
        if (string.IsNullOrWhiteSpace(value)) return;
        if (!Directory.Exists(value)) return;
        _savesDirectory = value;

        _saves = new Dictionary<string, DirectoryInfo>(Directory.GetDirectories(_savesDirectory).Select(x => new KeyValuePair<string, DirectoryInfo>(x, new DirectoryInfo(x))));

        tbSavePath.Text = _savesDirectory;
      }
    }

    private Dictionary<string, DirectoryInfo> _saves = new Dictionary<string, DirectoryInfo>();
    public Dictionary<string, DirectoryInfo> Saves { get => _saves; }



    public MainForm()
    {
      InitializeComponent();
    }

    private void btnChooseSavePath_Click(object sender, EventArgs e)
    {
      using (var folderBrowser = new FolderBrowserDialog())
      {
        if (folderBrowser.ShowDialog() != DialogResult.OK) return;
        if (string.IsNullOrWhiteSpace(folderBrowser.SelectedPath)) return;
        SavesDirectory = folderBrowser.SelectedPath;
      }
      LoadAndCheckSavesDirectory();
      if(Saves.Count <= 0)
      {
        MessageBox.Show("No saves found, make sure to select the folder called \"Saves\" that contains all farms and not one specific farm.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    protected void LoadAndCheckSavesDirectory()
    {
      lvSaves.Items.Clear();
      lvSaves.Items.AddRange(Saves.Select(x => new ListViewItem(new[] {
        x.Value.Name,
        x.Value.LastWriteTime.ToString("dd.mm.yyyy HH:MM:ss")
      }.ToArray())
      { Tag = x }).ToArray());
    }

    protected void ChooseSave(ListViewItem? item)
    {
      if (item == null) return;
      if (item.Tag == null) return;
      var savePath = (KeyValuePair<string, DirectoryInfo>)item.Tag;
      var files = Directory.GetFiles(savePath.Key);
      if (files.Length == 0) return;

      var fileGameSaveInfo = files.FirstOrDefault(x => Path.GetFileName(x).Contains("SaveGameInfo") && !Path.GetFileName(x).Contains("old"), "");
      var fileWorld = files.FirstOrDefault(x => Regex.IsMatch(Path.GetFileName(x), "(.+)_(\\d+)") && !Path.GetFileName(x).Contains("old"), "");
      if (string.IsNullOrEmpty(fileGameSaveInfo) || string.IsNullOrEmpty(fileWorld)) return;


      var xmlGameSaveInfo = new XmlDocument();
      xmlGameSaveInfo.Load(fileGameSaveInfo);
      var xmlWorld = new XmlDocument();
      xmlWorld.Load(fileWorld);

      try
      {
        var xmlPlayer = xmlWorld.GetChildWithName("SaveGame")?.GetChildWithName("player");
        if (xmlPlayer == null) return;

        tbMainPlayer.Text = xmlPlayer.GetChildWithName("name")?.InnerText ?? "";

        lvFarmhands.Items.Clear();
        var xmlFarmhands = xmlWorld.GetChildWithName("SaveGame")?.GetChildWithName("farmhands");
        if (xmlFarmhands == null) return;

        foreach (XmlNode farmerNode in xmlFarmhands.ChildNodes)
        {
          var farmerName = farmerNode.GetChildWithName("name")?.InnerText ?? "";
          if (string.IsNullOrWhiteSpace(farmerName)) continue;
          WorldInfo info = new WorldInfo();
          info.SavePath = savePath;
          info.DocSaveGameInfo = xmlGameSaveInfo;
          info.FileNameSaveGameInfo = fileGameSaveInfo;
          info.DocWorld = xmlWorld;
          info.FileNameWorld = fileWorld;
          info.Player = xmlPlayer;
          info.Farmhands = xmlFarmhands;
          info.Farmer = farmerNode;
          ListViewItem newFarmerItem = new ListViewItem(new[]
          {
            farmerName,
            farmerNode.GetChildWithName("Gender")?.InnerText ?? ""
          }.ToArray())
          {
            Tag = info
          };
          lvFarmhands.Items.Add(newFarmerItem);
        }
      }
      catch (Exception ex)
      {
        tbMainPlayer.Text = "File not valid";
        lvFarmhands.Items.Clear();
        Debug.WriteLine(ex.ToString());
      }
    }

    internal struct WorldInfo
    {
      public KeyValuePair<string, DirectoryInfo> SavePath { get; set; }
      public XmlDocument? DocSaveGameInfo { get; set; }
      public string FileNameSaveGameInfo { get; set; }
      public XmlDocument? DocWorld { get; set; }
      public string FileNameWorld { get; set; }
      public XmlNode? Player { get; set; }
      public XmlNode? Farmhands { get; set; }
      public XmlNode? Farmer { get; set; }
    }

    private void lvSaves_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      ChooseSave(e.Item);
    }

    private void btnMakeMainPlayer_Click(object sender, EventArgs e)
    {
      ChangeMainPlayer();
    }

    protected void ChangeMainPlayer()
    {
      if (lvFarmhands.SelectedItems.Count <= 0 || lvFarmhands.SelectedItems[0] == null)
      {
        MessageBox.Show("Please select a farmer from the list to make them the main player.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      var info = (WorldInfo)lvFarmhands.SelectedItems[0].Tag!;

      // Backup machen
      //if (MessageBox.Show("Create backup?", "Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
      //{
      //  var backupDirectory = info.SavePath.Key + "_backup_" + DateTime.Now.ToString("yyyy-mm-dd_HH-MM-ss");
      //  Directory.CreateDirectory(backupDirectory);
      //  foreach (var file in Directory.GetFiles(info.SavePath.Key))
      //  {
      //    File.Copy(file, Path.Combine(backupDirectory, Path.GetFileName(file)));
      //  }
      //  MessageBox.Show("Backup created:" + Environment.NewLine + Environment.NewLine + backupDirectory, null, MessageBoxButtons.OK, MessageBoxIcon.Information);
      //}

      var saveName = Path.GetFileName(info.SavePath.Key);
      var farmName = saveName.Split("_")[0];
      var farmNumber = saveName.Split("_")[1];
      var newFarmName = farmName + "Swap";
      var newFarmFolderName = newFarmName + "_" + farmNumber;
      var newSavePath = Path.Combine(Directory.GetParent(info.SavePath.Key).FullName, newFarmFolderName);

      // Change nodes in SaveGameInfo xml
      XmlNode? gameSaveInfoFarmer = null;
      foreach (XmlNode node in info.DocSaveGameInfo!.ChildNodes)
      {
        if (node.Name != "Farmer") continue;
        gameSaveInfoFarmer = node;
        break;
      }

      if (gameSaveInfoFarmer == null) return;
      gameSaveInfoFarmer.RemoveAll();
      foreach (XmlNode node in info.Farmer!.ChildNodes)
      {
        var newNode = gameSaveInfoFarmer.OwnerDocument.ImportNode(node, true);
        if (newNode.Name == "farmName") newNode.InnerText = newFarmName;
        gameSaveInfoFarmer.AppendChild(newNode);
      }

      // Change nodes in world xml
      XmlNode oldWorldPlayer = info.Player!.Clone();
      info.Player!.RemoveAll();
      foreach (XmlNode node in info.Farmer!.ChildNodes)
      {
        var newNode = node.Clone();
        if (newNode.Name == "farmName") newNode.InnerText = newFarmName;
        info.Player!.AppendChild(newNode);
      }
      info.Farmer.RemoveAll();
      foreach (XmlNode node in oldWorldPlayer.ChildNodes)
      {
        var newNode = node.Clone();
        if (newNode.Name == "farmName") newNode.InnerText = newFarmName;
        info.Farmer!.AppendChild(newNode);
      }
      if (Directory.Exists(newSavePath))
      {
        int i = 1;
        do
        {
          i++;
          newFarmFolderName = newFarmName + i.ToString() + "_" + farmNumber;
          newSavePath = Path.Combine(Directory.GetParent(info.SavePath.Key).FullName, newFarmFolderName);
        } while (Directory.Exists(newSavePath));
      }
      var newFileSaveGameInfo = Path.Combine(newSavePath, Path.GetFileName(info.FileNameSaveGameInfo));
      var newWorldName = Path.Combine(newSavePath, newFarmFolderName);
      if (!Directory.Exists(newSavePath)) Directory.CreateDirectory(newSavePath);
      info.DocSaveGameInfo!.Save(newFileSaveGameInfo);
      info.DocWorld!.Save(newWorldName);

      SavesDirectory = SavesDirectory;
      LoadAndCheckSavesDirectory();
      tbMainPlayer.Text = string.Empty;
      lvFarmhands.Items.Clear();

      var msgSuccess = "Success: A new save file has been created in directory:" + Environment.NewLine + Environment.NewLine + newSavePath + Environment.NewLine + Environment.NewLine + "Open folder now?";
      if (MessageBox.Show(msgSuccess, "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
      {
        try
        {
          Process.Start("explorer.exe", newSavePath);
        }
        catch (Exception e)
        {
          MessageBox.Show("Error while trying to open path " + newSavePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }
  }
}
