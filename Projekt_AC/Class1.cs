using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.GraphicsInterface;
using System.Data.SqlClient;

namespace Projekt_AC
{
    public class Class1
    {
        [CommandMethod("AP_Main")]
        public static void ShowForm()
        {
            Form_main forma = new Form_main();
            forma.Show();
        }

        [CommandMethod("AP_Warstwa")]
        public void warstwa()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable tb = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord record = trans.GetObject(tb[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                LayerTable laytb = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                LayerTableRecord layrec = new LayerTableRecord();

                string cNazwa = "LINIA";

                if (laytb.Has(cNazwa) == false)
                {
                    layrec.Color = Color.FromRgb(255, 0, 0);
                    layrec.Name = cNazwa;
                    laytb.UpgradeOpen();
                    laytb.Add(layrec);
                    trans.AddNewlyCreatedDBObject(layrec, true);
                }
                else
                {
                    Application.ShowAlertDialog("Warstwa już istnieje.");
                }
                trans.Commit();
            }

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable tb = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord record = trans.GetObject(tb[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                LayerTable laytb = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                LayerTableRecord layrec = new LayerTableRecord();

                string cNazwa = "OKRAG";

                if (laytb.Has(cNazwa) == false)
                {
                    layrec.Color = Color.FromRgb(0, 255, 0);
                    layrec.Name = cNazwa;
                    laytb.UpgradeOpen();
                    laytb.Add(layrec);
                    trans.AddNewlyCreatedDBObject(layrec, true);
                }
                else
                {
                    Application.ShowAlertDialog("Warstwa już istnieje.");
                }
                trans.Commit();
            }
        }

        [CommandMethod("AP_Linia")]
        public void AP_Linia()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable ltb = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForRead);

                db.Clayer = ltb["Linia"];

                BlockTable tb = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord record = trans.GetObject(tb[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                PromptPointOptions kom = new PromptPointOptions("Wskaż pierwszy punkt: ");
                PromptPointResult rez = ed.GetPoint(kom);
                Point3d punkt = rez.Value;

                PromptPointOptions kom2 = new PromptPointOptions("Wskaż drugi punkt: ");
                kom2.UseBasePoint = true;
                kom2.BasePoint = punkt;

                PromptPointResult rez2 = ed.GetPoint(kom2);
                Point3d punkt2 = rez2.Value;

                Line linia = new Line(punkt, punkt2);
                linia.SetDatabaseDefaults();
                record.AppendEntity(linia);

                trans.AddNewlyCreatedDBObject(linia, true);
                trans.Commit();
            }
        }

        [CommandMethod("AP_Okrag")]
        public void AP_OKrag()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                LayerTable ltb = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForRead);

                db.Clayer = ltb["OKRAG"];

                BlockTable tb = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord record = trans.GetObject(tb[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                PromptPointOptions kom = new PromptPointOptions("Wskaż punkt centralny: ");
                PromptPointResult rez = ed.GetPoint(kom);
                Point3d punkt = rez.Value;

                PromptPointOptions kom2 = new PromptPointOptions("Wskaż długość promienia: ");
                kom2.UseBasePoint = true;
                kom2.BasePoint = punkt;
                PromptPointResult rez2 = ed.GetPoint(kom2);
                Point3d punkt2 = rez2.Value;

                Circle okrag = new Circle();
                okrag.Center = punkt;
                okrag.Radius = punkt.DistanceTo(punkt2);

                record.AppendEntity(okrag);
                trans.AddNewlyCreatedDBObject(okrag, true);
                trans.Commit();
            }
        }

        [CommandMethod("AP_Zlicz")]
        public void AP_Zlicz()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            PromptResult prompt = ed.GetString("Podaj nazwe warstwy: ");

            if (prompt.Status == PromptStatus.OK)
            {
                ObjectIdCollection ents = Obiekty_Na_Warstwie(prompt.StringResult);
                Application.ShowAlertDialog("Znaleziono " + ents.Count + " obiektów na warstwie " + prompt.StringResult); 
            }
        } 

        private ObjectIdCollection Obiekty_Na_Warstwie(string nazwa_warstwy)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            TypedValue[] tvs = new TypedValue[1] {new TypedValue((int)DxfCode.LayerName,nazwa_warstwy)};

            SelectionFilter filter = new SelectionFilter(tvs);

            PromptSelectionResult prompt = ed.SelectAll(filter);

            if (prompt.Status == PromptStatus.OK)
                return new ObjectIdCollection(prompt.Value.GetObjectIds());
            else
                return new ObjectIdCollection();
        }

        [CommandMethod("AP_Przenies")]
        public void AP_Przenies()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptStringOptions kom = new PromptStringOptions("Podaj nazwe warstwy pierwszej: ");
            kom.AllowSpaces = true;

            PromptResult prompt = ed.GetString(kom);

            if (prompt.Status != PromptStatus.OK)
                return;

            string nazwa_warstwy = prompt.StringResult;

            TypedValue[] tvs = new TypedValue[1];
            tvs[0] = new TypedValue((int)DxfCode.LayerName, nazwa_warstwy);
            
            SelectionFilter filter = new SelectionFilter(tvs);

            PromptSelectionResult rez = ed.SelectAll(filter);

            int zlicz = 0;

            if (rez.Status == PromptStatus.OK)
                zlicz = rez.Value.Count;

            if (rez.Status == PromptStatus.OK || rez.Status == PromptStatus.Error)
            {
                Application.ShowAlertDialog("Znaleziono " + zlicz + " na warstwie " + nazwa_warstwy);
                if (zlicz > 0)
                {
                    kom.Message = "Podaj warstwe docelowa";

                    prompt = ed.GetString(kom);

                    if (prompt.Status != PromptStatus.OK || prompt.StringResult == "")
                        return;
                    
                    string nowa_nazwa_warstwy = prompt.StringResult;

                    Transaction trans = db.TransactionManager.StartTransaction();

                    using (trans)
                    {
                        LayerTable lt = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForRead);
                        if (!lt.Has(nowa_nazwa_warstwy))
                            Application.ShowAlertDialog("Nie znaleziono warstwy.");
                        else
                        {
                            int zlicz_zmiany = 0;
                            ObjectId lid = lt[nowa_nazwa_warstwy];

                            foreach (ObjectId id in rez.Value.GetObjectIds())
                            {
                                Entity ent = (Entity)trans.GetObject(id, OpenMode.ForWrite);
                                ent.LayerId = lid;
                                zlicz_zmiany++;
                            }
                            Application.ShowAlertDialog("Przeniesiono " + zlicz_zmiany + " obietków z warstwy " + nazwa_warstwy + " do warstwy " + nowa_nazwa_warstwy);
                        }
                        trans.Commit();
                    }
                }
            }
        }
    }
}


