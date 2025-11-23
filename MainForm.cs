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

            rbDom.Checked = true;
        }

        private void btnLoadXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "XML Files|*.xml" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _xmlPath = dlg.FileName;
                txtXmlPath.Text = _xmlPath;
            }
        }

        private void btnLoadXsl_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "XSL Files|*.xslt" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _xslPath = dlg.FileName;
                txtXslPath.Text = _xslPath;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_xmlPath)) return;


            if (rbDom.Checked) _context.SetStrategy(new DomSearchStrategy());
            else if (rbSax.Checked) _context.SetStrategy(new SaxSearchStrategy());
            else if (rbLinq.Checked) _context.SetStrategy(new LinqSearchStrategy());


            Student criteria = new Student
            {
                Name = txtNameFilter.Text.Trim(),        
                Discipline = txtSubjectFilter.Text.Trim() 
            };

            try
            {
                List<string> results = _context.PerformSearch(_xmlPath, criteria);

                listBoxResults.Items.Clear();
                foreach (var res in results)
                {
                    listBoxResults.Items.Add(res);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void btnTransform_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = "HTML Files|*.html" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _context.TransformToHtml(_xmlPath, _xslPath, dlg.FileName);
                    MessageBox.Show("Трансформація успішна!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }
        }
    }
}
