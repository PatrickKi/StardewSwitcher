namespace StardewSwitcher
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      tbSavePath = new TextBox();
      lblSavePath = new Label();
      btnChooseSavePath = new Button();
      lvSaves = new ListView();
      colSaveName = new ColumnHeader();
      colLastChange = new ColumnHeader();
      lblMainPlayer = new Label();
      lblFarmhands = new Label();
      tbMainPlayer = new TextBox();
      lvFarmhands = new ListView();
      colName = new ColumnHeader();
      colGender = new ColumnHeader();
      btnMakeMainPlayer = new Button();
      SuspendLayout();
      // 
      // tbSavePath
      // 
      tbSavePath.BackColor = SystemColors.Menu;
      tbSavePath.BorderStyle = BorderStyle.FixedSingle;
      tbSavePath.Font = new Font("Lato", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
      tbSavePath.Location = new Point(182, 12);
      tbSavePath.Name = "tbSavePath";
      tbSavePath.PlaceholderText = "Usually C:\\Users\\Username\\AppData\\Roaming\\StardewValley\\Saves";
      tbSavePath.ReadOnly = true;
      tbSavePath.Size = new Size(830, 27);
      tbSavePath.TabIndex = 0;
      tbSavePath.TabStop = false;
      tbSavePath.WordWrap = false;
      // 
      // lblSavePath
      // 
      lblSavePath.AutoSize = true;
      lblSavePath.Font = new Font("Lato", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lblSavePath.Location = new Point(12, 14);
      lblSavePath.Name = "lblSavePath";
      lblSavePath.Size = new Size(164, 19);
      lblSavePath.TabIndex = 1;
      lblSavePath.Text = "Path of save directory";
      // 
      // btnChooseSavePath
      // 
      btnChooseSavePath.Font = new Font("Lato", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
      btnChooseSavePath.Location = new Point(1018, 12);
      btnChooseSavePath.Name = "btnChooseSavePath";
      btnChooseSavePath.Size = new Size(234, 238);
      btnChooseSavePath.TabIndex = 2;
      btnChooseSavePath.Text = "Choose saves directory";
      btnChooseSavePath.UseVisualStyleBackColor = true;
      btnChooseSavePath.Click += btnChooseSavePath_Click;
      // 
      // lvSaves
      // 
      lvSaves.AllowColumnReorder = true;
      lvSaves.Columns.AddRange(new ColumnHeader[] { colSaveName, colLastChange });
      lvSaves.FullRowSelect = true;
      lvSaves.Location = new Point(182, 45);
      lvSaves.MultiSelect = false;
      lvSaves.Name = "lvSaves";
      lvSaves.Size = new Size(830, 205);
      lvSaves.TabIndex = 4;
      lvSaves.UseCompatibleStateImageBehavior = false;
      lvSaves.View = View.Details;
      lvSaves.ItemSelectionChanged += lvSaves_ItemSelectionChanged;
      // 
      // colSaveName
      // 
      colSaveName.Text = "Save Name";
      colSaveName.Width = 200;
      // 
      // colLastChange
      // 
      colLastChange.Text = "Last Change";
      colLastChange.Width = 150;
      // 
      // lblMainPlayer
      // 
      lblMainPlayer.AutoSize = true;
      lblMainPlayer.Font = new Font("Lato", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lblMainPlayer.Location = new Point(84, 318);
      lblMainPlayer.Name = "lblMainPlayer";
      lblMainPlayer.Size = new Size(92, 19);
      lblMainPlayer.TabIndex = 6;
      lblMainPlayer.Text = "Main player";
      // 
      // lblFarmhands
      // 
      lblFarmhands.AutoSize = true;
      lblFarmhands.Font = new Font("Lato", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lblFarmhands.Location = new Point(90, 406);
      lblFarmhands.Name = "lblFarmhands";
      lblFarmhands.Size = new Size(86, 19);
      lblFarmhands.TabIndex = 7;
      lblFarmhands.Text = "Farmhands";
      // 
      // tbMainPlayer
      // 
      tbMainPlayer.BackColor = SystemColors.Menu;
      tbMainPlayer.BorderStyle = BorderStyle.FixedSingle;
      tbMainPlayer.Font = new Font("Lato", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
      tbMainPlayer.Location = new Point(182, 316);
      tbMainPlayer.Name = "tbMainPlayer";
      tbMainPlayer.ReadOnly = true;
      tbMainPlayer.Size = new Size(830, 27);
      tbMainPlayer.TabIndex = 8;
      tbMainPlayer.TabStop = false;
      tbMainPlayer.WordWrap = false;
      // 
      // lvFarmhands
      // 
      lvFarmhands.AllowColumnReorder = true;
      lvFarmhands.Columns.AddRange(new ColumnHeader[] { colName, colGender });
      lvFarmhands.FullRowSelect = true;
      lvFarmhands.Location = new Point(182, 359);
      lvFarmhands.MultiSelect = false;
      lvFarmhands.Name = "lvFarmhands";
      lvFarmhands.Size = new Size(830, 149);
      lvFarmhands.TabIndex = 9;
      lvFarmhands.UseCompatibleStateImageBehavior = false;
      lvFarmhands.View = View.Details;
      // 
      // colName
      // 
      colName.Text = "Character name";
      colName.Width = 200;
      // 
      // colGender
      // 
      colGender.Text = "Gender";
      // 
      // btnMakeMainPlayer
      // 
      btnMakeMainPlayer.Font = new Font("Lato", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
      btnMakeMainPlayer.Location = new Point(1018, 359);
      btnMakeMainPlayer.Name = "btnMakeMainPlayer";
      btnMakeMainPlayer.Size = new Size(234, 149);
      btnMakeMainPlayer.TabIndex = 10;
      btnMakeMainPlayer.Text = "Make main player and save as new folder";
      btnMakeMainPlayer.UseVisualStyleBackColor = true;
      btnMakeMainPlayer.Click += btnMakeMainPlayer_Click;
      // 
      // MainForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = Color.White;
      ClientSize = new Size(1264, 519);
      Controls.Add(btnMakeMainPlayer);
      Controls.Add(lvFarmhands);
      Controls.Add(tbMainPlayer);
      Controls.Add(lblFarmhands);
      Controls.Add(lblMainPlayer);
      Controls.Add(lvSaves);
      Controls.Add(btnChooseSavePath);
      Controls.Add(lblSavePath);
      Controls.Add(tbSavePath);
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      Name = "MainForm";
      StartPosition = FormStartPosition.CenterScreen;
      Text = "StardewSwitcher";
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private TextBox tbSavePath;
    private Label lblSavePath;
    private Button btnChooseSavePath;
    private ListView lvSaves;
    private ColumnHeader colSaveName;
    private ColumnHeader colLastChange;
    private Label lblMainPlayer;
    private Label lblFarmhands;
    private TextBox tbMainPlayer;
    private ListView lvFarmhands;
    private ColumnHeader colName;
    private ColumnHeader colGender;
    private Button btnMakeMainPlayer;
  }
}
