namespace Projekt_AC
{
    partial class Form_main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.polacz = new System.Windows.Forms.Button();
            this.data_src = new System.Windows.Forms.TextBox();
            this.tabela = new System.Windows.Forms.TextBox();
            this.dodaj_okrag = new System.Windows.Forms.Button();
            this.usun_okregi = new System.Windows.Forms.Button();
            this.dane = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.TextBox();
            this.info = new System.Windows.Forms.Button();
            this.backup = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.do_update = new System.Windows.Forms.TextBox();
            this.CSV = new System.Windows.Forms.Button();
            this.xml = new System.Windows.Forms.Button();
            this.user = new System.Windows.Forms.TextBox();
            this.haslo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dane_grid = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.info_line = new System.Windows.Forms.Button();
            this.csv_line = new System.Windows.Forms.Button();
            this.xml_line = new System.Windows.Forms.Button();
            this.delete_line = new System.Windows.Forms.Button();
            this.update_line = new System.Windows.Forms.Button();
            this.dodaj_line = new System.Windows.Forms.Button();
            this.pobierz_line = new System.Windows.Forms.Button();
            this.dane_grid_line = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Informacje_arc = new System.Windows.Forms.Button();
            this.csv_arc = new System.Windows.Forms.Button();
            this.xml_arc = new System.Windows.Forms.Button();
            this.del_arc = new System.Windows.Forms.Button();
            this.update_arc = new System.Windows.Forms.Button();
            this.dodaj_arc = new System.Windows.Forms.Button();
            this.pobierz_arc = new System.Windows.Forms.Button();
            this.dane_arc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.polaczenie = new System.Windows.Forms.Button();
            this.przywroc = new System.Windows.Forms.Button();
            this.Baza = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dane)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.Baza.SuspendLayout();
            this.SuspendLayout();
            // 
            // polacz
            // 
            this.polacz.Location = new System.Drawing.Point(19, 51);
            this.polacz.Name = "polacz";
            this.polacz.Size = new System.Drawing.Size(75, 34);
            this.polacz.TabIndex = 0;
            this.polacz.Text = "Pobierz";
            this.polacz.UseVisualStyleBackColor = true;
            this.polacz.Click += new System.EventHandler(this.polacz_Click);
            // 
            // data_src
            // 
            this.data_src.Location = new System.Drawing.Point(86, 25);
            this.data_src.Name = "data_src";
            this.data_src.Size = new System.Drawing.Size(134, 20);
            this.data_src.TabIndex = 1;
            this.data_src.Text = ".\\MS_SQLSERVER";
            this.data_src.TextChanged += new System.EventHandler(this.data_src_TextChanged);
            // 
            // tabela
            // 
            this.tabela.Location = new System.Drawing.Point(86, 62);
            this.tabela.Name = "tabela";
            this.tabela.Size = new System.Drawing.Size(134, 20);
            this.tabela.TabIndex = 2;
            this.tabela.Text = "PROJEKT";
            // 
            // dodaj_okrag
            // 
            this.dodaj_okrag.Location = new System.Drawing.Point(19, 91);
            this.dodaj_okrag.Name = "dodaj_okrag";
            this.dodaj_okrag.Size = new System.Drawing.Size(75, 23);
            this.dodaj_okrag.TabIndex = 3;
            this.dodaj_okrag.Text = "Dodaj";
            this.dodaj_okrag.UseVisualStyleBackColor = true;
            this.dodaj_okrag.Click += new System.EventHandler(this.dodaj_okrag_Click);
            // 
            // usun_okregi
            // 
            this.usun_okregi.Location = new System.Drawing.Point(19, 156);
            this.usun_okregi.Name = "usun_okregi";
            this.usun_okregi.Size = new System.Drawing.Size(75, 23);
            this.usun_okregi.TabIndex = 4;
            this.usun_okregi.Text = "Usuń";
            this.usun_okregi.UseVisualStyleBackColor = true;
            this.usun_okregi.Click += new System.EventHandler(this.usun_okregi_Click);
            // 
            // dane
            // 
            this.dane.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dane.Location = new System.Drawing.Point(36, 282);
            this.dane.Name = "dane";
            this.dane.Size = new System.Drawing.Size(302, 150);
            this.dane.TabIndex = 5;
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(264, 53);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(100, 20);
            this.id.TabIndex = 6;
            // 
            // info
            // 
            this.info.Location = new System.Drawing.Point(19, 251);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(75, 23);
            this.info.TabIndex = 7;
            this.info.Text = "Informacje";
            this.info.UseVisualStyleBackColor = true;
            this.info.Click += new System.EventHandler(this.info_Click);
            // 
            // backup
            // 
            this.backup.Location = new System.Drawing.Point(383, 339);
            this.backup.Name = "backup";
            this.backup.Size = new System.Drawing.Size(75, 46);
            this.backup.TabIndex = 8;
            this.backup.Text = "Backup";
            this.backup.UseVisualStyleBackColor = true;
            this.backup.Click += new System.EventHandler(this.backup_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(19, 120);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 9;
            this.update.Text = "Aktualizuj";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // do_update
            // 
            this.do_update.Location = new System.Drawing.Point(264, 106);
            this.do_update.Name = "do_update";
            this.do_update.Size = new System.Drawing.Size(100, 20);
            this.do_update.TabIndex = 10;
            // 
            // CSV
            // 
            this.CSV.Location = new System.Drawing.Point(19, 222);
            this.CSV.Name = "CSV";
            this.CSV.Size = new System.Drawing.Size(75, 23);
            this.CSV.TabIndex = 11;
            this.CSV.Text = "CSV";
            this.CSV.UseVisualStyleBackColor = true;
            this.CSV.Click += new System.EventHandler(this.CSV_Click);
            // 
            // xml
            // 
            this.xml.Location = new System.Drawing.Point(19, 189);
            this.xml.Name = "xml";
            this.xml.Size = new System.Drawing.Size(75, 23);
            this.xml.TabIndex = 12;
            this.xml.Text = "XML";
            this.xml.UseVisualStyleBackColor = true;
            this.xml.Click += new System.EventHandler(this.xml_Click);
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(86, 100);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(134, 20);
            this.user.TabIndex = 13;
            // 
            // haslo
            // 
            this.haslo.Location = new System.Drawing.Point(86, 134);
            this.haslo.Name = "haslo";
            this.haslo.PasswordChar = '*';
            this.haslo.Size = new System.Drawing.Size(134, 20);
            this.haslo.TabIndex = 14;
            this.haslo.TextChanged += new System.EventHandler(this.haslo_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dane_grid);
            this.groupBox1.Controls.Add(this.polacz);
            this.groupBox1.Controls.Add(this.dodaj_okrag);
            this.groupBox1.Controls.Add(this.update);
            this.groupBox1.Controls.Add(this.CSV);
            this.groupBox1.Controls.Add(this.xml);
            this.groupBox1.Controls.Add(this.usun_okregi);
            this.groupBox1.Controls.Add(this.info);
            this.groupBox1.Location = new System.Drawing.Point(383, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(111, 286);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Okręgi";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dane_grid
            // 
            this.dane_grid.Location = new System.Drawing.Point(19, 22);
            this.dane_grid.Name = "dane_grid";
            this.dane_grid.Size = new System.Drawing.Size(75, 23);
            this.dane_grid.TabIndex = 13;
            this.dane_grid.Text = "Dane";
            this.dane_grid.UseVisualStyleBackColor = true;
            this.dane_grid.Click += new System.EventHandler(this.dane_grid_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.info_line);
            this.groupBox2.Controls.Add(this.csv_line);
            this.groupBox2.Controls.Add(this.xml_line);
            this.groupBox2.Controls.Add(this.delete_line);
            this.groupBox2.Controls.Add(this.update_line);
            this.groupBox2.Controls.Add(this.dodaj_line);
            this.groupBox2.Controls.Add(this.pobierz_line);
            this.groupBox2.Controls.Add(this.dane_grid_line);
            this.groupBox2.Location = new System.Drawing.Point(500, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(93, 286);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Linie";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // info_line
            // 
            this.info_line.Location = new System.Drawing.Point(7, 245);
            this.info_line.Name = "info_line";
            this.info_line.Size = new System.Drawing.Size(75, 23);
            this.info_line.TabIndex = 7;
            this.info_line.Text = "Informacje";
            this.info_line.UseVisualStyleBackColor = true;
            this.info_line.Click += new System.EventHandler(this.info_line_Click);
            // 
            // csv_line
            // 
            this.csv_line.Location = new System.Drawing.Point(7, 216);
            this.csv_line.Name = "csv_line";
            this.csv_line.Size = new System.Drawing.Size(75, 23);
            this.csv_line.TabIndex = 6;
            this.csv_line.Text = "CSV";
            this.csv_line.UseVisualStyleBackColor = true;
            this.csv_line.Click += new System.EventHandler(this.csv_line_Click);
            // 
            // xml_line
            // 
            this.xml_line.Location = new System.Drawing.Point(7, 183);
            this.xml_line.Name = "xml_line";
            this.xml_line.Size = new System.Drawing.Size(75, 23);
            this.xml_line.TabIndex = 5;
            this.xml_line.Text = "XML";
            this.xml_line.UseVisualStyleBackColor = true;
            this.xml_line.Click += new System.EventHandler(this.xml_line_Click);
            // 
            // delete_line
            // 
            this.delete_line.Location = new System.Drawing.Point(7, 150);
            this.delete_line.Name = "delete_line";
            this.delete_line.Size = new System.Drawing.Size(75, 23);
            this.delete_line.TabIndex = 4;
            this.delete_line.Text = "Usuń";
            this.delete_line.UseVisualStyleBackColor = true;
            this.delete_line.Click += new System.EventHandler(this.delete_line_Click);
            // 
            // update_line
            // 
            this.update_line.Location = new System.Drawing.Point(7, 114);
            this.update_line.Name = "update_line";
            this.update_line.Size = new System.Drawing.Size(75, 23);
            this.update_line.TabIndex = 3;
            this.update_line.Text = "Aktualizuj";
            this.update_line.UseVisualStyleBackColor = true;
            this.update_line.Click += new System.EventHandler(this.update_line_Click);
            // 
            // dodaj_line
            // 
            this.dodaj_line.Location = new System.Drawing.Point(7, 85);
            this.dodaj_line.Name = "dodaj_line";
            this.dodaj_line.Size = new System.Drawing.Size(75, 23);
            this.dodaj_line.TabIndex = 2;
            this.dodaj_line.Text = "Dodaj";
            this.dodaj_line.UseVisualStyleBackColor = true;
            this.dodaj_line.Click += new System.EventHandler(this.dodaj_line_Click);
            // 
            // pobierz_line
            // 
            this.pobierz_line.Location = new System.Drawing.Point(7, 50);
            this.pobierz_line.Name = "pobierz_line";
            this.pobierz_line.Size = new System.Drawing.Size(75, 23);
            this.pobierz_line.TabIndex = 1;
            this.pobierz_line.Text = "Pobierz";
            this.pobierz_line.UseVisualStyleBackColor = true;
            this.pobierz_line.Click += new System.EventHandler(this.pobierz_line_Click);
            // 
            // dane_grid_line
            // 
            this.dane_grid_line.Location = new System.Drawing.Point(7, 16);
            this.dane_grid_line.Name = "dane_grid_line";
            this.dane_grid_line.Size = new System.Drawing.Size(75, 23);
            this.dane_grid_line.TabIndex = 0;
            this.dane_grid_line.Text = "Dane";
            this.dane_grid_line.UseVisualStyleBackColor = true;
            this.dane_grid_line.Click += new System.EventHandler(this.dane_grid_line_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Informacje_arc);
            this.groupBox3.Controls.Add(this.csv_arc);
            this.groupBox3.Controls.Add(this.xml_arc);
            this.groupBox3.Controls.Add(this.del_arc);
            this.groupBox3.Controls.Add(this.update_arc);
            this.groupBox3.Controls.Add(this.dodaj_arc);
            this.groupBox3.Controls.Add(this.pobierz_arc);
            this.groupBox3.Controls.Add(this.dane_arc);
            this.groupBox3.Location = new System.Drawing.Point(599, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(90, 281);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Łuki";
            // 
            // Informacje_arc
            // 
            this.Informacje_arc.Location = new System.Drawing.Point(7, 245);
            this.Informacje_arc.Name = "Informacje_arc";
            this.Informacje_arc.Size = new System.Drawing.Size(75, 23);
            this.Informacje_arc.TabIndex = 7;
            this.Informacje_arc.Text = "Informacje";
            this.Informacje_arc.UseVisualStyleBackColor = true;
            this.Informacje_arc.Click += new System.EventHandler(this.Informacje_arc_Click);
            // 
            // csv_arc
            // 
            this.csv_arc.Location = new System.Drawing.Point(7, 217);
            this.csv_arc.Name = "csv_arc";
            this.csv_arc.Size = new System.Drawing.Size(75, 23);
            this.csv_arc.TabIndex = 6;
            this.csv_arc.Text = "CSV";
            this.csv_arc.UseVisualStyleBackColor = true;
            this.csv_arc.Click += new System.EventHandler(this.csv_arc_Click);
            // 
            // xml_arc
            // 
            this.xml_arc.Location = new System.Drawing.Point(7, 184);
            this.xml_arc.Name = "xml_arc";
            this.xml_arc.Size = new System.Drawing.Size(75, 23);
            this.xml_arc.TabIndex = 5;
            this.xml_arc.Text = "XML";
            this.xml_arc.UseVisualStyleBackColor = true;
            this.xml_arc.Click += new System.EventHandler(this.xml_arc_Click);
            // 
            // del_arc
            // 
            this.del_arc.Location = new System.Drawing.Point(7, 150);
            this.del_arc.Name = "del_arc";
            this.del_arc.Size = new System.Drawing.Size(75, 23);
            this.del_arc.TabIndex = 4;
            this.del_arc.Text = "Usuń";
            this.del_arc.UseVisualStyleBackColor = true;
            this.del_arc.Click += new System.EventHandler(this.del_arc_Click);
            // 
            // update_arc
            // 
            this.update_arc.Location = new System.Drawing.Point(7, 115);
            this.update_arc.Name = "update_arc";
            this.update_arc.Size = new System.Drawing.Size(75, 23);
            this.update_arc.TabIndex = 3;
            this.update_arc.Text = "Aktualizuj";
            this.update_arc.UseVisualStyleBackColor = true;
            this.update_arc.Click += new System.EventHandler(this.update_arc_Click);
            // 
            // dodaj_arc
            // 
            this.dodaj_arc.Location = new System.Drawing.Point(7, 80);
            this.dodaj_arc.Name = "dodaj_arc";
            this.dodaj_arc.Size = new System.Drawing.Size(75, 23);
            this.dodaj_arc.TabIndex = 2;
            this.dodaj_arc.Text = "Dodaj";
            this.dodaj_arc.UseVisualStyleBackColor = true;
            this.dodaj_arc.Click += new System.EventHandler(this.dodaj_arc_Click);
            // 
            // pobierz_arc
            // 
            this.pobierz_arc.Location = new System.Drawing.Point(7, 45);
            this.pobierz_arc.Name = "pobierz_arc";
            this.pobierz_arc.Size = new System.Drawing.Size(75, 23);
            this.pobierz_arc.TabIndex = 1;
            this.pobierz_arc.Text = "Pobierz";
            this.pobierz_arc.UseVisualStyleBackColor = true;
            this.pobierz_arc.Click += new System.EventHandler(this.pobierz_arc_Click);
            // 
            // dane_arc
            // 
            this.dane_arc.Location = new System.Drawing.Point(7, 16);
            this.dane_arc.Name = "dane_arc";
            this.dane_arc.Size = new System.Drawing.Size(75, 23);
            this.dane_arc.TabIndex = 0;
            this.dane_arc.Text = "Dane";
            this.dane_arc.UseVisualStyleBackColor = true;
            this.dane_arc.Click += new System.EventHandler(this.dane_arc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Serwer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Baza danych";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Użytkownik";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Hasło";
            // 
            // polaczenie
            // 
            this.polaczenie.Location = new System.Drawing.Point(86, 171);
            this.polaczenie.Name = "polaczenie";
            this.polaczenie.Size = new System.Drawing.Size(75, 38);
            this.polaczenie.TabIndex = 22;
            this.polaczenie.Text = "Połączenie";
            this.polaczenie.UseVisualStyleBackColor = true;
            this.polaczenie.Click += new System.EventHandler(this.polaczenie_Click);
            // 
            // przywroc
            // 
            this.przywroc.Location = new System.Drawing.Point(480, 339);
            this.przywroc.Name = "przywroc";
            this.przywroc.Size = new System.Drawing.Size(75, 44);
            this.przywroc.TabIndex = 23;
            this.przywroc.Text = "Przywróć dane";
            this.przywroc.UseVisualStyleBackColor = true;
            this.przywroc.Click += new System.EventHandler(this.przywroc_Click);
            // 
            // Baza
            // 
            this.Baza.Controls.Add(this.label1);
            this.Baza.Controls.Add(this.data_src);
            this.Baza.Controls.Add(this.polaczenie);
            this.Baza.Controls.Add(this.label2);
            this.Baza.Controls.Add(this.label4);
            this.Baza.Controls.Add(this.tabela);
            this.Baza.Controls.Add(this.label3);
            this.Baza.Controls.Add(this.haslo);
            this.Baza.Controls.Add(this.user);
            this.Baza.Location = new System.Drawing.Point(12, 8);
            this.Baza.Name = "Baza";
            this.Baza.Size = new System.Drawing.Size(234, 249);
            this.Baza.TabIndex = 24;
            this.Baza.TabStop = false;
            this.Baza.Text = "Baza";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(261, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Id do modyfikacji";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(261, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Wartość do aktualizacji";
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 444);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Baza);
            this.Controls.Add(this.przywroc);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.do_update);
            this.Controls.Add(this.backup);
            this.Controls.Add(this.id);
            this.Controls.Add(this.dane);
            this.Name = "Form_main";
            this.Text = "Form_main";
            this.Load += new System.EventHandler(this.Form_main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dane)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.Baza.ResumeLayout(false);
            this.Baza.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button polacz;
        private System.Windows.Forms.TextBox data_src;
        private System.Windows.Forms.TextBox tabela;
        private System.Windows.Forms.Button dodaj_okrag;
        private System.Windows.Forms.Button usun_okregi;
        private System.Windows.Forms.DataGridView dane;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Button info;
        private System.Windows.Forms.Button backup;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.TextBox do_update;
        private System.Windows.Forms.Button CSV;
        private System.Windows.Forms.Button xml;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.TextBox haslo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button polaczenie;
        private System.Windows.Forms.Button przywroc;
        private System.Windows.Forms.Button dane_grid;
        private System.Windows.Forms.GroupBox Baza;
        private System.Windows.Forms.Button dane_grid_line;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button pobierz_line;
        private System.Windows.Forms.Button dodaj_line;
        private System.Windows.Forms.Button update_line;
        private System.Windows.Forms.Button delete_line;
        private System.Windows.Forms.Button xml_line;
        private System.Windows.Forms.Button csv_line;
        private System.Windows.Forms.Button info_line;
        private System.Windows.Forms.Button dane_arc;
        private System.Windows.Forms.Button pobierz_arc;
        private System.Windows.Forms.Button dodaj_arc;
        private System.Windows.Forms.Button update_arc;
        private System.Windows.Forms.Button del_arc;
        private System.Windows.Forms.Button xml_arc;
        private System.Windows.Forms.Button Informacje_arc;
        private System.Windows.Forms.Button csv_arc;
    }
}