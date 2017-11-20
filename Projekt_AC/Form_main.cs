using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.GraphicsInterface;
using Microsoft.SqlServer.Management.Smo;
using DB_AC = Autodesk.AutoCAD.DatabaseServices;
using SQL_CLIENT = System.Data.SqlClient;
using INFO = Microsoft.SqlServer.Management.Smo;
using System.Configuration;
using CSV = System.Data;
using System.Web;
using System.Xml;

namespace Projekt_AC
{
    public partial class Form_main : Form
    {
        public Form_main()
        {
            InitializeComponent();
        }

        private void polacz_Click(object sender, EventArgs e)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            DB_AC.Database db = document.Database;
            Editor editor = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            document.LockDocument();

            try
            {
                SQL_CLIENT.SqlDataAdapter adapter = new SQL_CLIENT.SqlDataAdapter("Select * from CIRCLE", @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true");
                System.Data.DataTable tb = new System.Data.DataTable();
                adapter.Fill(tb);
                dane.DataSource = tb;

                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Liczba odczytanych okręgów: " + tb.Rows.Count);

                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    BlockTable blockTb = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord record = trans.GetObject(blockTb[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        Circle circle = new Circle();
                        circle.Radius = Convert.ToDouble(tb.Rows[i]["radius"]);
                        Point3d cCenter = new Point3d(Convert.ToDouble(tb.Rows[i]["centerX"]), Convert.ToDouble(tb.Rows[i]["centerY"]), Convert.ToDouble(tb.Rows[i]["centerZ"]));
                        circle.Center = cCenter;

                        record.AppendEntity(circle);
                        trans.AddNewlyCreatedDBObject(circle, true);
                    }
                    trans.Commit();
                }
            }
            catch (System.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Błąd odczytu: " + ex.Message);
            }
            this.Close();
        }

        private void dodaj_okrag_Click(object sender, EventArgs e)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            DB_AC.Database db = document.Database;
            Editor editor = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            document.LockDocument();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
            PromptEntityOptions kom = new PromptEntityOptions("Wskaż okrąg do zapisu: ");
            PromptEntityResult rez = editor.GetEntity(kom);

            if (rez.Status == PromptStatus.OK)
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        Circle circle = trans.GetObject(rez.ObjectId, OpenMode.ForWrite) as Circle;
                        trans.Commit();

                        string GUID = Guid.NewGuid().ToString();

                        double centerX = circle.Center.X;
                        double centerY = circle.Center.Y;
                        double centerZ = circle.Center.Z;

                        double radius = circle.Radius;

                        string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
                        SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
                        string cZapytanie = "INSERT INTO CIRCLE (GUID, centerX, centerY, centerZ, radius) VALUES (@GUID,@centerX,@centerY,@centerZ,@radius)";

                        SQL_CLIENT.SqlCommand cmd = new SQL_CLIENT.SqlCommand(cZapytanie, con);
                        cmd.Parameters.AddWithValue("@GUID", GUID);
                        cmd.Parameters.AddWithValue("@centerX", centerX);
                        cmd.Parameters.AddWithValue("@centerY", centerY);
                        cmd.Parameters.AddWithValue("@centerZ", centerZ);
                        cmd.Parameters.AddWithValue("@radius", radius);

                        con.Open();
                        int cIle = cmd.ExecuteNonQuery();
                        con.Close();

                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Ilość zapisanych okręgów: " + cIle.ToString());
                    }
                    catch (System.Exception ex)
                    {
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Błąd zapisu: " + ex.Message);
                    }
                }
            }
        }

        private void usun_okregi_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            con.Open();
            string cZapytanie = "DELETE CIRCLE WHERE ID=" + id.Text;
            SQL_CLIENT.SqlCommand cmd = new SQL_CLIENT.SqlCommand(cZapytanie, con);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Usunieto");
            }
            else
            {
                MessageBox.Show("Źle");
            }
            con.Close();
        }

        private void info_Click(object sender, EventArgs e)
        {
            Server myServer = new Server(@".\MS_SQLSERVER"); //lub (local)
            //Autentykacja Windows
            myServer.ConnectionContext.LoginSecure = true;
            //Autentykacja SQL
            myServer.ConnectionContext.Connect();
            
            INFO.Database cBaza = myServer.Databases["PROJEKT"];
            
            INFO.Table cTab = cBaza.Tables["CIRCLE"];
            foreach (INFO.Column item in cTab.Columns)
            {
                MessageBox.Show("Nazwa kolumny: " + item.Name + "\nTyp danych: " + item.DataType.ToString());
            }

            INFO.Column cKol = cTab.Columns["id"];
            MessageBox.Show("Czy kolumna id jest w kluczu głównym tabeli: " + cKol.InPrimaryKey.ToString());
            MessageBox.Show("Czy kolumna id jest kluczem obcym: " + cKol.IsForeignKey.ToString());
            MessageBox.Show("Czy kolumna id jest kolumną wyliczaną: " + cKol.Computed.ToString());
            MessageBox.Show("Czy kolumna id zezwala na NULL-e: " + cKol.Nullable.ToString());

            int licznik = 0;
            foreach (Microsoft.SqlServer.Management.Smo.View view in cBaza.Views)
            {
                if (licznik < 5)
                {
                    MessageBox.Show("Nazwa widoku: " + view.Name + "\nData utworzenia: " + view.CreateDate.ToShortDateString());
                    licznik++;
                }
            }

            //Procedury skałdowe
            int licznik2 = 0;
            foreach (StoredProcedure sp in cBaza.StoredProcedures)
            {
                if (licznik2 < 5)
                {
                    MessageBox.Show("Nazwa procedury: " + sp.Name + "\nLiczba parametrów: " + sp.Parameters.Count.ToString());
                    licznik2++;
                }
            }

            //przegladanie użytkowników DB
            foreach (User user in cBaza.Users)
            {
                MessageBox.Show("User: " + user.Name + "\nLogin: " + user.Login + "\nType: " + user.UserType);
            }
        }

        private void backup_Click(object sender, EventArgs e)
        {
            Server srv = new Server(@".\MS_SQLSERVER");
            INFO.Database db = srv.Databases["PROJEKT"];

            string cNazwaPliku = "\\Backup_" + db.Name + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bak";
            //string cSciezka_LOKALNA = @"C:\" +cNazwaPliku;
            string cSciezka_LOKALNA = srv.BackupDirectory + cNazwaPliku;

            Backup bkpDBFull = new Backup();
            bkpDBFull.Action = BackupActionType.Database;
            bkpDBFull.Database = db.Name;
            bkpDBFull.Devices.AddDevice(cSciezka_LOKALNA, DeviceType.File);
            bkpDBFull.BackupSetName = db.Name + " Backup";
            bkpDBFull.BackupSetDescription = db.Name + " - Full Backup";
            bkpDBFull.Initialize = false;

            bkpDBFull.SqlBackup(srv);
            MessageBox.Show("Backup wykonano pomyślnie");
        }

        private void update_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            con.Open();
            string cZapytanie = "UPDATE CIRCLE SET centerZ='" + do_update.Text.ToString() + "' WHERE ID=" + id.Text;
            SQL_CLIENT.SqlCommand cmd = new SQL_CLIENT.SqlCommand(cZapytanie, con);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Zaktualizowano");
            }
            else
            {
                MessageBox.Show("Źle");
            }
            con.Close();
        }

        private void CSV_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            con.Open();
            SQL_CLIENT.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM CIRCLE", con);
            SQL_CLIENT.SqlDataReader dr = cmd.ExecuteReader();

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(@"C:\Users\Marcin Jackowiak\Desktop\Projekt_AC\circle.csv"))
            {
                fs.WriteLine("sep=,");
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    string name = dr.GetName(i);
                    if (name.Contains(","))
                    {
                        name = "\"" + name + "\"";
                    }
                    fs.WriteLine(name + ",");
                }
                fs.WriteLine();

                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(","))
                        {
                            value = "\"" + value + "\"";
                        }
                        fs.Write(value + ",");
                    }
                    fs.WriteLine();
                }
                fs.Close();
            }
        }

        private void xml_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            const string strsql = "SELECT * FROM CIRCLE";

            using (SQL_CLIENT.SqlCommand com = new SQL_CLIENT.SqlCommand (strsql, con))
            {
                SQL_CLIENT.SqlDataAdapter da = new SQL_CLIENT.SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ds.Tables[0].WriteXml(@"C:\Users\Marcin Jackowiak\Desktop\Projekt_AC\circle.xml");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void przywroc_Click(object sender, EventArgs e)
        {
            Server srv = new Server(@".\MS_SQLSERVER");
            INFO.Database db = srv.Databases["PROJEKT"];

            Restore restoreDB = new Restore();
            restoreDB.Database = db.Name;
            restoreDB.Action = RestoreActionType.Database;
            restoreDB.Devices.AddDevice(@"C:\baza.bak", DeviceType.File);
            restoreDB.ReplaceDatabase = true;
            restoreDB.NoRecovery = true;
            restoreDB.SqlRestore(srv);
        }

        private void polaczenie_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            try
            {
                con.Open();
                MessageBox.Show("Połącznie otwarte");
                con.Close();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Błąd");
            }
        }

        private void dane_grid_Click(object sender, EventArgs e)
        {
            SQL_CLIENT.SqlDataAdapter adapter = new SQL_CLIENT.SqlDataAdapter("Select * from CIRCLE", @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true");
            System.Data.DataTable tb = new System.Data.DataTable();
            adapter.Fill(tb);
            dane.DataSource = tb;
        }

        private void data_src_TextChanged(object sender, EventArgs e)
        {

        }

        private void dane_grid_line_Click(object sender, EventArgs e)
        {
            SQL_CLIENT.SqlDataAdapter adapter = new SQL_CLIENT.SqlDataAdapter("Select * from LINE", @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true");
            System.Data.DataTable tb = new System.Data.DataTable();
            adapter.Fill(tb);
            dane.DataSource = tb;
        }

        private void pobierz_line_Click(object sender, EventArgs e)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            DB_AC.Database db = document.Database;
            Editor editor = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            document.LockDocument();

            try
            {
                SQL_CLIENT.SqlDataAdapter adapter = new SQL_CLIENT.SqlDataAdapter("Select * from LINE", @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true");
                System.Data.DataTable tb = new System.Data.DataTable();
                adapter.Fill(tb);
                dane.DataSource = tb;

                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Liczba odczytanych linii: " + tb.Rows.Count);

                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    BlockTable blockTb = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord record = trans.GetObject(blockTb[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        Line line = new Line();
                        Point3d cStart = new Point3d(Convert.ToDouble(tb.Rows[i]["startX"]), Convert.ToDouble(tb.Rows[i]["startY"]), Convert.ToDouble(tb.Rows[i]["startZ"]));
                        Point3d cEnd = new Point3d(Convert.ToDouble(tb.Rows[i]["endX"]), Convert.ToDouble(tb.Rows[i]["endY"]), Convert.ToDouble(tb.Rows[i]["endZ"]));
                        line.StartPoint = cStart;
                        line.EndPoint = cEnd;

                        record.AppendEntity(line);
                        trans.AddNewlyCreatedDBObject(line, true);
                    }
                    trans.Commit();
                }
            }
            catch (System.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Błąd odczytu: " + ex.Message);
            }
            this.Close();
        }

        private void dodaj_line_Click(object sender, EventArgs e)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            DB_AC.Database db = document.Database;
            Editor editor = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            document.LockDocument();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
            PromptEntityOptions kom = new PromptEntityOptions("Wskaż linie do zapisu: ");
            PromptEntityResult rez = editor.GetEntity(kom);

            if (rez.Status == PromptStatus.OK)
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        Line line = trans.GetObject(rez.ObjectId, OpenMode.ForWrite) as Line;
                        trans.Commit();

                        string GUID = Guid.NewGuid().ToString();
                        double startX = line.StartPoint.X;
                        double startY = line.StartPoint.Y;
                        double startZ = line.StartPoint.Z;
                        double endX = line.EndPoint.X;
                        double endY = line.EndPoint.Y;
                        double endZ = line.EndPoint.Z;

                        string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
                        SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
                        string cZapytanie = "INSERT INTO LINE (GUID, startX, startY, startZ, endX, endY, endZ) VALUES (@GUID, @startX, @startY, @startZ, @endX, @endY, @endZ)";

                        SQL_CLIENT.SqlCommand cmd = new SQL_CLIENT.SqlCommand(cZapytanie, con);
                        cmd.Parameters.AddWithValue("@GUID", GUID);
                        cmd.Parameters.AddWithValue("@startX", startX);
                        cmd.Parameters.AddWithValue("@startY", startY);
                        cmd.Parameters.AddWithValue("@startZ", startZ);
                        cmd.Parameters.AddWithValue("@endX", endX);
                        cmd.Parameters.AddWithValue("@endY", endY);
                        cmd.Parameters.AddWithValue("@endZ", endZ);

                        con.Open();
                        int cIle = cmd.ExecuteNonQuery();
                        con.Close();

                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Ilość zapisanych linii: " + cIle.ToString());
                    }
                    catch (System.Exception ex)
                    {
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Błąd zapisu: " + ex.Message);
                    }
                }
            }
        }

        private void update_line_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            con.Open();
            string cZapytanie = "UPDATE LINE SET startZ='" + do_update.Text.ToString() + "' WHERE ID=" + id.Text;
            SQL_CLIENT.SqlCommand cmd = new SQL_CLIENT.SqlCommand(cZapytanie, con);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Zaktualizowano");
            }
            else
            {
                MessageBox.Show("Źle");
            }
            con.Close();
        }

        private void delete_line_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            con.Open();
            string cZapytanie = "DELETE LINE WHERE ID=" + id.Text;
            SQL_CLIENT.SqlCommand cmd = new SQL_CLIENT.SqlCommand(cZapytanie, con);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Usunieto");
            }
            else
            {
                MessageBox.Show("Źle");
            }
            con.Close();
        }

        private void xml_line_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            const string strsql = "SELECT * FROM LINE";

            using (SQL_CLIENT.SqlCommand com = new SQL_CLIENT.SqlCommand(strsql, con))
            {
                SQL_CLIENT.SqlDataAdapter da = new SQL_CLIENT.SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ds.Tables[0].WriteXml(@"C:\Users\Marcin Jackowiak\Desktop\Projekt_AC\line.xml");
            }
        }

        private void csv_line_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            con.Open();
            SQL_CLIENT.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM LINE", con);
            SQL_CLIENT.SqlDataReader dr = cmd.ExecuteReader();

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(@"C:\Users\Marcin Jackowiak\Desktop\Projekt_AC\line.csv"))
            {
                fs.WriteLine("sep=,");
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    string name = dr.GetName(i);
                    if (name.Contains(","))
                    {
                        name = "\"" + name + "\"";
                    }
                    fs.WriteLine(name + ",");
                }
                fs.WriteLine();

                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(","))
                        {
                            value = "\"" + value + "\"";
                        }
                        fs.Write(value + ",");
                    }
                    fs.WriteLine();
                }
                fs.Close();
            }
        }

        private void info_line_Click(object sender, EventArgs e)
        {
            Server myServer = new Server(@".\MS_SQLSERVER"); //lub (local)
            //Autentykacja Windows
            myServer.ConnectionContext.LoginSecure = true;
            //Autentykacja SQL
            myServer.ConnectionContext.Connect();


            INFO.Database cBaza = myServer.Databases["PROJEKT"];

            INFO.Table cTab = cBaza.Tables["LINE"];
            foreach (INFO.Column item in cTab.Columns)
            {
                MessageBox.Show("Nazwa kolumny: " + item.Name + "\nTyp danych: " + item.DataType.ToString());
            }

            INFO.Column cKol = cTab.Columns["id"];
            MessageBox.Show("Czy kolumna id jest w kluczu głównym tabeli: " + cKol.InPrimaryKey.ToString());
            MessageBox.Show("Czy kolumna id jest kluczem obcym: " + cKol.IsForeignKey.ToString());
            MessageBox.Show("Czy kolumna id jest kolumną wyliczaną: " + cKol.Computed.ToString());
            MessageBox.Show("Czy kolumna id zezwala na NULL-e: " + cKol.Nullable.ToString());

            int licznik = 0;
            foreach (Microsoft.SqlServer.Management.Smo.View view in cBaza.Views)
            {
                if (licznik < 5)
                {
                    MessageBox.Show("Nazwa widoku: " + view.Name + "\nData utworzenia: " + view.CreateDate.ToShortDateString());
                    licznik++;
                }
            }

            //Procedury skałdowe
            int licznik2 = 0;
            foreach (StoredProcedure sp in cBaza.StoredProcedures)
            {
                if (licznik2 < 5)
                {
                    MessageBox.Show("Nazwa procedury: " + sp.Name + "\nLiczba parametrów: " + sp.Parameters.Count.ToString());
                    licznik2++;
                }
            }

            //przegladanie użytkowników DB
            foreach (User user in cBaza.Users)
            {
                MessageBox.Show("User: " + user.Name + "\nLogin: " + user.Login + "\nType: " + user.UserType);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form_main_Load(object sender, EventArgs e)
        {

        }

        private void dane_arc_Click(object sender, EventArgs e)
        {
            SQL_CLIENT.SqlDataAdapter adapter = new SQL_CLIENT.SqlDataAdapter("Select * from ARC", @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true");
            System.Data.DataTable tb = new System.Data.DataTable();
            adapter.Fill(tb);
            dane.DataSource = tb;
        }

        private void pobierz_arc_Click(object sender, EventArgs e)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            DB_AC.Database db = document.Database;
            Editor editor = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            document.LockDocument();

            try
            {
                SQL_CLIENT.SqlDataAdapter adapter = new SQL_CLIENT.SqlDataAdapter("Select * from ARC", @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true");
                System.Data.DataTable tb = new System.Data.DataTable();
                adapter.Fill(tb);
                dane.DataSource = tb;

                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Liczba odczytanych lukow: " + tb.Rows.Count);

                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    BlockTable blockTb = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord record = trans.GetObject(blockTb[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        Arc arc = new Arc();
                        arc.Radius = Convert.ToDouble(tb.Rows[i]["radius"]);
                        Point3d cCenter = new Point3d(Convert.ToDouble(tb.Rows[i]["centerX"]), Convert.ToDouble(tb.Rows[i]["centerY"]), Convert.ToDouble(tb.Rows[i]["centerZ"]));
                        arc.Center = cCenter;
                        arc.StartAngle = Convert.ToDouble(tb.Rows[i]["startAngle"]);
                        arc.StartAngle = Convert.ToDouble(tb.Rows[i]["endAngle"]);

                        record.AppendEntity(arc);
                        trans.AddNewlyCreatedDBObject(arc, true);
                    }
                    trans.Commit();
                }
            }
            catch (System.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Błąd odczytu: " + ex.Message);
            }
            this.Close();
        }

        private void dodaj_arc_Click(object sender, EventArgs e)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            DB_AC.Database db = document.Database;
            Editor editor = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            document.LockDocument();
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
            PromptEntityOptions kom = new PromptEntityOptions("Wskaż luk do zapisu: ");
            PromptEntityResult rez = editor.GetEntity(kom);

            if (rez.Status == PromptStatus.OK)
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        Arc arc = trans.GetObject(rez.ObjectId, OpenMode.ForWrite) as Arc;
                        trans.Commit();

                        string GUID = Guid.NewGuid().ToString();
                        double centerX = arc.Center.X;
                        double centerY = arc.Center.Y;
                        double centerZ = arc.Center.Z;
                        double radius = arc.Radius;
                        double startAngle = arc.StartAngle;
                        double endAngle = arc.EndAngle;

                        string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";

                        SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
                        string cZapytanie = "INSERT INTO ARC (GUID, centerX, centerY, centerZ, radius, startAngle, endAngle) VALUES (@GUID, @centerX, @centerY, @centerZ, @radius, @startAngle, @endAngle)";

                        SQL_CLIENT.SqlCommand cmd = new SQL_CLIENT.SqlCommand(cZapytanie, con);
                        cmd.Parameters.AddWithValue("@GUID", GUID);
                        cmd.Parameters.AddWithValue("@centerX", centerX);
                        cmd.Parameters.AddWithValue("@centerY", centerY);
                        cmd.Parameters.AddWithValue("@centerZ", centerZ);
                        cmd.Parameters.AddWithValue("@radius", radius);
                        cmd.Parameters.AddWithValue("@startAngle", startAngle);
                        cmd.Parameters.AddWithValue("@endAngle", endAngle);

                        con.Open();
                        int cIle = cmd.ExecuteNonQuery();
                        con.Close();

                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Ilość zapisanych łuków: " + cIle.ToString());
                    }
                    catch (System.Exception ex)
                    {
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Błąd zapisu: " + ex.Message);
                    }
                }
            }
        }

        private void update_arc_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            con.Open();
            string cZapytanie = "UPDATE ARC SET centerZ='" + do_update.Text.ToString() + "' WHERE ID=" + id.Text;
            SQL_CLIENT.SqlCommand cmd = new SQL_CLIENT.SqlCommand(cZapytanie, con);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Zaktualizowano");
            }
            else
            {
                MessageBox.Show("Źle");
            }
            con.Close();
        }

        private void del_arc_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            con.Open();
            string cZapytanie = "DELETE ARC WHERE ID=" + id.Text;
            SQL_CLIENT.SqlCommand cmd = new SQL_CLIENT.SqlCommand(cZapytanie, con);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Usunieto");
            }
            else
            {
                MessageBox.Show("Źle");
            }
            con.Close();
        }

        private void xml_arc_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            const string strsql = "SELECT * FROM ARC";

            using (SQL_CLIENT.SqlCommand com = new SQL_CLIENT.SqlCommand(strsql, con))
            {
                SQL_CLIENT.SqlDataAdapter da = new SQL_CLIENT.SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ds.Tables[0].WriteXml(@"C:\Users\Marcin Jackowiak\Desktop\Projekt_AC\arc.xml");
            }
        }

        private void csv_arc_Click(object sender, EventArgs e)
        {
            string cConnectiongSting = @"Data Source =" + data_src.Text + "; Initial Catalog =" + tabela.Text + "; User ID =" + user.Text + ";Password =" + haslo.Text + "; Integrated Security = true";
            SQL_CLIENT.SqlConnection con = new SQL_CLIENT.SqlConnection(cConnectiongSting);
            con.Open();
            SQL_CLIENT.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM ARC", con);
            SQL_CLIENT.SqlDataReader dr = cmd.ExecuteReader();

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(@"C:\Users\Marcin Jackowiak\Desktop\Projekt_AC\arc.cvs"))
            {
                fs.WriteLine("sep=,");
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    string name = dr.GetName(i);
                    if (name.Contains(","))
                    {
                        name = "\"" + name + "\"";
                    }
                    fs.WriteLine(name + ",");
                }
                fs.WriteLine();

                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(","))
                        {
                            value = "\"" + value + "\"";
                        }
                        fs.Write(value + ",");
                    }
                    fs.WriteLine();
                }
                fs.Close();
            }
        }

        private void Informacje_arc_Click(object sender, EventArgs e)
        {
            Server myServer = new Server(@".\MS_SQLSERVER"); //lub (local)
            //Autentykacja Windows
            myServer.ConnectionContext.LoginSecure = true;
            //Autentykacja SQL
            myServer.ConnectionContext.Connect();

            INFO.Database cBaza = myServer.Databases["PROJEKT"];
            INFO.Table cTab = cBaza.Tables["ARC"];
            foreach (INFO.Column item in cTab.Columns)
            {
                MessageBox.Show("Nazwa kolumny: " + item.Name + "\nTyp danych: " + item.DataType.ToString());
            }

            INFO.Column cKol = cTab.Columns["id"];
            MessageBox.Show("Czy kolumna id jest w kluczu głównym tabeli: " + cKol.InPrimaryKey.ToString());
            MessageBox.Show("Czy kolumna id jest kluczem obcym: " + cKol.IsForeignKey.ToString());
            MessageBox.Show("Czy kolumna id jest kolumną wyliczaną: " + cKol.Computed.ToString());
            MessageBox.Show("Czy kolumna id zezwala na NULL-e: " + cKol.Nullable.ToString());

            int licznik = 0;
            foreach (Microsoft.SqlServer.Management.Smo.View view in cBaza.Views)
            {
                if (licznik < 5)
                {
                    MessageBox.Show("Nazwa widoku: " + view.Name + "\nData utworzenia: " + view.CreateDate.ToShortDateString());
                    licznik++;
                }
            }

            //Procedury skałdowe
            int licznik2 = 0;
            foreach (StoredProcedure sp in cBaza.StoredProcedures)
            {
                if (licznik2 < 5)
                {
                    MessageBox.Show("Nazwa procedury: " + sp.Name + "\nLiczba parametrów: " + sp.Parameters.Count.ToString());
                    licznik2++;
                }
            }

            //przegladanie użytkowników DB
            foreach (User user in cBaza.Users)
            {
                MessageBox.Show("User: " + user.Name + "\nLogin: " + user.Login + "\nType: " + user.UserType);
            }
        }

        private void haslo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

