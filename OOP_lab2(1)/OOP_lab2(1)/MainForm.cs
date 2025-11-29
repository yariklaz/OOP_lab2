using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace XmlStudentManager
{
    public partial class MainForm : Form
    {
        private XmlContext _context;
        private string _xmlPath;
        private string _xslPath;

        public MainForm()
        {
            InitializeComponent();
            _context = new XmlContext();
        }

        private void btnLoadXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "XML Files|*.xml" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _xmlPath = dlg.FileName;
                txtXmlPath.Text = _xmlPath;
                PopulateFilters();
            }
        }

        private void PopulateFilters()
        {
            try
            {
                cmbNameFilter.Items.Clear();
                cmbFacultyFilter.Items.Clear();
                cmbSubjectFilter.Items.Clear();

                var names = _context.GetUniqueValues(_xmlPath, "Student", "Name");
                cmbNameFilter.Items.AddRange(names.ToArray());

                var faculties = _context.GetUniqueValues(_xmlPath, "Faculty", "Name");
                cmbFacultyFilter.Items.AddRange(faculties.ToArray());

                var subjects = _context.GetUniqueValues(_xmlPath, "Subject", "Title");
                cmbSubjectFilter.Items.AddRange(subjects.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при читанні даних для списків: " + ex.Message);
            }
        }

        private void btnLoadXsl_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "XSL Files (*.xsl; *.xslt)|*.xsl;*.xslt" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _xslPath = dlg.FileName;
                txtXslPath.Text = _xslPath;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_xmlPath))
            {
                MessageBox.Show("Спочатку завантажте XML файл!");
                return;
            }

            if (rbDom.Checked) _context.SetStrategy(new DomSearchStrategy());
            else if (rbSax.Checked) _context.SetStrategy(new SaxSearchStrategy());
            else if (rbLinq.Checked) _context.SetStrategy(new LinqSearchStrategy());

            // Зчитуємо значення з випадаючих списків
            StudentSearchCriteria criteria = new StudentSearchCriteria
            {
                Name = cmbNameFilter.Text.Trim(),
                Discipline = cmbSubjectFilter.Text.Trim(),
                Faculty = cmbFacultyFilter.Text.Trim()
            };

            try
            {
                List<string> results = _context.PerformSearch(_xmlPath, criteria);

                listBoxResults.Items.Clear();
                if (results.Count == 0) listBoxResults.Items.Add("Нічого не знайдено.");

                foreach (var res in results) listBoxResults.Items.Add(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при пошуку: " + ex.Message);
            }
        }

        private void btnTransform_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_xmlPath) || string.IsNullOrEmpty(_xslPath))
            {
                MessageBox.Show("Завантажте XML та XSL файли!");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog { Filter = "HTML Files|*.html" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _context.TransformToHtml(_xmlPath, _xslPath, dlg.FileName);
                    MessageBox.Show("HTML файл успішно створено!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка трансформації: " + ex.Message);
                }
            }
        }
    }
}