using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace App_CSV_XML_JSON_Files_in_C_
{
    public partial class Form1 : Form
    {
        public string fileCSVPath, fileXMLPath, fileJSONPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void AddColum_Click(object sender, EventArgs e)
        {
            using (FormNombreColumna formNombreColumna = new FormNombreColumna())
            {
                if (formNombreColumna.ShowDialog() == DialogResult.OK)
                {
                    string nombreNuevaColumna = formNombreColumna.NombreColumna;

                    if (!string.IsNullOrWhiteSpace(nombreNuevaColumna))
                    {
                        ListFilesData.Columns.Add(nombreNuevaColumna, nombreNuevaColumna);
                    }
                }
            }
        }

        private void AddRow_Click(object sender, EventArgs e)
        {
            ListFilesData.Rows.Add();
        }

        private void DeleteColum_Click(object sender, EventArgs e)
        {
            if (ListFilesData.Columns.Count > 0)
            {
                ListFilesData.Columns.RemoveAt(ListFilesData.Columns.Count - 1);
            }
        }

        private void DeleteRow_Click(object sender, EventArgs e)
        {
            if (ListFilesData.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow filaSeleccionada in ListFilesData.SelectedRows)
                {
                    if (filaSeleccionada.IsNewRow)
                    {
                        return;
                    }

                    ListFilesData.Rows.Remove(filaSeleccionada);
                }

                return;
            }

            MessageBox.Show("No as seleccionado una fila completa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void OpenDialog(ref string filePath, string filter)
        {
            ListFilesData.Rows.Clear();
            ListFilesData.Columns.Clear();

            var fileDialog = new OpenFileDialog
            {
                Filter = filter
            };

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            filePath = fileDialog.FileName;

            NombreArchivoLBL.Text = $"Archivo: {fileDialog.SafeFileName}";
        }

        public bool SaveDialog(ref string filePath, string filter)
        {
            if (ListFilesData.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            var saveFileTem = new SaveFileDialog
            {
                Filter = filter
            };

            if (saveFileTem.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show($"Datos no guardados desde el archivo {Path.GetFileName(saveFileTem.FileName)} incorrectamente.", "Operación cancelada", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return true;
            }

            filePath = saveFileTem.FileName;

            NombreArchivoLBL.Text = $"Archivo: {Path.GetFileName(saveFileTem.FileName)}";

            return false;
        }

        private void CSV_OpenMenuItem_Click(object sender, EventArgs e)
        {
            OpenDialog(ref fileCSVPath, "Archivos CSV (*.csv)|*.csv");

            using (var CSVReader = new StreamReader(fileCSVPath))
            {
                string primeraLinea = CSVReader.ReadLine();

                if (primeraLinea == null)
                {
                    return;
                }

                string[] encabezados = primeraLinea.Split(',');

                ListFilesData.Columns.Clear();

                foreach (var encabezado in encabezados)
                {
                    ListFilesData.Columns.Add(encabezado, encabezado);
                }

                while (!CSVReader.EndOfStream)
                {
                    string line = CSVReader.ReadLine();
                    string[] propertyless = line.Split(',');

                    ListFilesData.Rows.Add(propertyless);
                }
            }

            MessageBox.Show("Datos cargados desde el archivo CSV correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CSV_SaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fileCSVPath))
            {
                if (SaveDialog(ref fileCSVPath, "Archivos CSV (*.csv)|*.csv"))
                {
                    return;
                }

                SaveFileCSV();
            }
            else
            {
                SaveFileCSV();
            }
        }

        private void CSV_SaveHowMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveDialog(ref fileCSVPath, "Archivos CSV (*.csv)|*.csv"))
            {
                return;
            }

            SaveFileCSV();
        }

        public void SaveFileCSV()
        {
            using (var CSVwriter = new StreamWriter(fileCSVPath))
            {
                for (int i = 0; i < ListFilesData.Columns.Count; i++)
                {
                    CSVwriter.Write(ListFilesData.Columns[i].HeaderText);

                    if (i < ListFilesData.Columns.Count - 1)
                    {
                        CSVwriter.Write(",");
                    }
                }

                CSVwriter.WriteLine();

                for (int i = 0; i < ListFilesData.Rows.Count; i++)
                {
                    for (int j = 0; j < ListFilesData.Columns.Count; j++)
                    {
                        CSVwriter.Write(ListFilesData.Rows[i].Cells[j].Value);

                        if (j < ListFilesData.Columns.Count - 1)
                        {
                            CSVwriter.Write(",");
                        }
                    }

                    CSVwriter.WriteLine();
                }
            }

            MessageBox.Show("Archivo CSV guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void XML_OpenMenuItem_Click(object sender, EventArgs e)
        {
            OpenDialog(ref fileXMLPath, "Archivos XML (*.xml)|*.xml");

            DataSet dataSet = new DataSet();

            dataSet.ReadXml(fileXMLPath);

            if (dataSet.Tables.Count > 0)
            {
                DataTable dataTable = dataSet.Tables[0];

                foreach (DataColumn column in dataTable.Columns)
                {
                    ListFilesData.Columns.Add(column.ColumnName, column.ColumnName);
                }

                foreach (DataRow row in dataTable.Rows)
                {
                    ListFilesData.Rows.Add(row.ItemArray);
                }

                MessageBox.Show("Datos cargados desde el archivo XML correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El archivo XML no contiene datos válidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void XML_SaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fileXMLPath))
            {
                if (SaveDialog(ref fileXMLPath, "Archivos XML (*.xml)|*.xml"))
                {
                    return;
                }

                SaveFileXML();
            }
            else
            {
                SaveFileXML();
            }
        }

        private void XML_SaveHowMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveDialog(ref fileXMLPath, "Archivos XML (*.xml)|*.xml"))
            {
                return;
            }

            SaveFileXML();
        }

        public void SaveFileXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDeclaration);

            XmlElement rootElement = xmlDoc.CreateElement("Datos");
            xmlDoc.AppendChild(rootElement);

            for (int i = 0; i < ListFilesData.Rows.Count; i++)
            {
                XmlElement rowElement = xmlDoc.CreateElement("Fila");

                for (int j = 0; j < ListFilesData.Columns.Count; j++)
                {
                    string columnName = ListFilesData.Columns[j].HeaderText;

                    string xmlElementName = GetValidXmlElementName(columnName);

                    string cellValue = Convert.ToString(ListFilesData.Rows[i].Cells[j].Value);

                    XmlElement cellElement = xmlDoc.CreateElement(xmlElementName);
                    cellElement.InnerText = cellValue;

                    rowElement.AppendChild(cellElement);
                }

                rootElement.AppendChild(rowElement);
            }

            xmlDoc.Save(fileXMLPath);

            MessageBox.Show("Datos guardados como archivo XML correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetValidXmlElementName(string input)
        {
            string validName = input.Replace(' ', '_');

            validName = new string(validName.Where(c => char.IsLetterOrDigit(c) || c == '_').ToArray());

            if (char.IsDigit(validName[0]))
            {
                validName = "_" + validName;
            }

            return validName;
        }

        private void JSONS_OpenMenuItem_Click(object sender, EventArgs e)
        {
            OpenDialog(ref fileJSONPath, "Archivos JSON (*.json)|*.json");

            string jsonDatos = System.IO.File.ReadAllText(fileJSONPath);

            var listaObjetos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonDatos);

            if (listaObjetos != null && listaObjetos.Count > 0)
            {
                foreach (string clave in listaObjetos[0].Keys)
                {
                    ListFilesData.Columns.Add(clave, clave);
                }

                foreach (var objetoFila in listaObjetos)
                {
                    object[] valores = objetoFila.Values.ToArray();

                    ListFilesData.Rows.Add(valores);
                }

                MessageBox.Show("Datos cargados desde el archivo JSON correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El archivo JSON no contiene datos válidos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void JSON_SaveMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fileJSONPath))
            {
                if (SaveDialog(ref fileJSONPath, "Archivos JSON (*.json)|*.json"))
                {
                    return;
                }

                SaveFileJSON();
            }
            else
            {
                SaveFileJSON();
            }
        }

        private void JSON_SaveHowMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveDialog(ref fileJSONPath, "Archivos JSON (*.json)|*.json"))
            {
                return;
            }

            SaveFileJSON();
        }

        public void SaveFileJSON()
        {
            var listaObjetos = new List<object>();

            foreach (DataGridViewRow fila in ListFilesData.Rows)
            {
                if (!fila.IsNewRow)
                {
                    var objetoFila = new Dictionary<string, object>();

                    foreach (DataGridViewCell celda in fila.Cells)
                    {
                        string nombreColumna = ListFilesData.Columns[celda.ColumnIndex].HeaderText;

                        objetoFila[nombreColumna] = celda.Value;
                    }

                    listaObjetos.Add(objetoFila);
                }
            }

            string jsonDatos = Newtonsoft.Json.JsonConvert.SerializeObject(listaObjetos, Newtonsoft.Json.Formatting.Indented);

            System.IO.File.WriteAllText(fileJSONPath, jsonDatos);

            MessageBox.Show("Datos guardados como archivo JSON correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
