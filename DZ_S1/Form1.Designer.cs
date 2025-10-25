namespace DZ_S1
{
    partial class Form1
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
            btnLoadDB = new Button();
            btnSaveDB = new Button();
            lvStorage = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            btnAdd = new Button();
            btnSell = new Button();
            btnCheckout = new Button();
            listView1 = new ListView();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            SuspendLayout();
            // 
            // btnLoadDB
            // 
            btnLoadDB.Location = new Point(23, 12);
            btnLoadDB.Name = "btnLoadDB";
            btnLoadDB.Size = new Size(195, 46);
            btnLoadDB.TabIndex = 0;
            btnLoadDB.Text = "Load database";
            btnLoadDB.UseVisualStyleBackColor = true;
            btnLoadDB.Click += btnLoadDB_Click;
            // 
            // btnSaveDB
            // 
            btnSaveDB.Location = new Point(240, 12);
            btnSaveDB.Name = "btnSaveDB";
            btnSaveDB.Size = new Size(195, 46);
            btnSaveDB.TabIndex = 1;
            btnSaveDB.Text = "Save database";
            btnSaveDB.UseVisualStyleBackColor = true;
            // 
            // lvStorage
            // 
            lvStorage.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            lvStorage.GridLines = true;
            lvStorage.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvStorage.Location = new Point(23, 73);
            lvStorage.Name = "lvStorage";
            lvStorage.Size = new Size(1311, 436);
            lvStorage.TabIndex = 2;
            lvStorage.UseCompatibleStateImageBehavior = false;
            lvStorage.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Text Name";
            columnHeader1.Width = 700;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Scancode";
            columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Price";
            columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Count";
            columnHeader4.Width = 150;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(23, 534);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(234, 46);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add to store";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnSell
            // 
            btnSell.Location = new Point(23, 602);
            btnSell.Name = "btnSell";
            btnSell.Size = new Size(234, 46);
            btnSell.TabIndex = 4;
            btnSell.Text = "Sell";
            btnSell.UseVisualStyleBackColor = true;
            // 
            // btnCheckout
            // 
            btnCheckout.Location = new Point(1049, 602);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(150, 46);
            btnCheckout.TabIndex = 6;
            btnCheckout.Text = "Checkout";
            btnCheckout.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader6, columnHeader7, columnHeader8, columnHeader9 });
            listView1.GridLines = true;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.Location = new Point(23, 654);
            listView1.Name = "listView1";
            listView1.Size = new Size(1311, 186);
            listView1.TabIndex = 7;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Text Name";
            columnHeader6.Width = 700;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Scancode";
            columnHeader7.Width = 150;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Price";
            columnHeader8.Width = 150;
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "Count";
            columnHeader9.Width = 150;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1607, 845);
            Controls.Add(listView1);
            Controls.Add(btnCheckout);
            Controls.Add(btnSell);
            Controls.Add(btnAdd);
            Controls.Add(lvStorage);
            Controls.Add(btnSaveDB);
            Controls.Add(btnLoadDB);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoadDB;
        private Button btnSaveDB;
        private ListView lvStorage;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private Button btnAdd;
        private Button btnSell;
        private Button btnCheckout;
        private ListView listView1;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
    }
}
