namespace XmlStudentManager
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnLoadXml = new System.Windows.Forms.Button();
            this.txtXmlPath = new System.Windows.Forms.TextBox();
            this.btnLoadXsl = new System.Windows.Forms.Button();
            this.txtXslPath = new System.Windows.Forms.TextBox();
            this.groupBoxStrategy = new System.Windows.Forms.GroupBox();
            this.rbLinq = new System.Windows.Forms.RadioButton();
            this.rbSax = new System.Windows.Forms.RadioButton();
            this.rbDom = new System.Windows.Forms.RadioButton();
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubjectFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameFilter = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnTransform = new System.Windows.Forms.Button();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.groupBoxStrategy.SuspendLayout();
            this.groupBoxFilters.SuspendLayout();
            this.SuspendLayout();

            this.btnLoadXml.Location = new System.Drawing.Point(12, 12);
            this.btnLoadXml.Name = "btnLoadXml";
            this.btnLoadXml.Size = new System.Drawing.Size(95, 23);
            this.btnLoadXml.TabIndex = 0;
            this.btnLoadXml.Text = "Завантажити XML";
            this.btnLoadXml.UseVisualStyleBackColor = true;
            this.btnLoadXml.Click += new System.EventHandler(this.btnLoadXml_Click);

            this.txtXmlPath.Location = new System.Drawing.Point(113, 14);
            this.txtXmlPath.Name = "txtXmlPath";
            this.txtXmlPath.ReadOnly = true;
            this.txtXmlPath.Size = new System.Drawing.Size(359, 20);
            this.txtXmlPath.TabIndex = 1;

            this.btnLoadXsl.Location = new System.Drawing.Point(12, 41);
            this.btnLoadXsl.Name = "btnLoadXsl";
            this.btnLoadXsl.Size = new System.Drawing.Size(95, 23);
            this.btnLoadXsl.TabIndex = 2;
            this.btnLoadXsl.Text = "Завантажити XSL";
            this.btnLoadXsl.UseVisualStyleBackColor = true;
            this.btnLoadXsl.Click += new System.EventHandler(this.btnLoadXsl_Click);

            this.txtXslPath.Location = new System.Drawing.Point(113, 43);
            this.txtXslPath.Name = "txtXslPath";
            this.txtXslPath.ReadOnly = true;
            this.txtXslPath.Size = new System.Drawing.Size(359, 20);
            this.txtXslPath.TabIndex = 3;

            this.groupBoxStrategy.Controls.Add(this.rbLinq);
            this.groupBoxStrategy.Controls.Add(this.rbSax);
            this.groupBoxStrategy.Controls.Add(this.rbDom);
            this.groupBoxStrategy.Location = new System.Drawing.Point(12, 80);
            this.groupBoxStrategy.Name = "groupBoxStrategy";
            this.groupBoxStrategy.Size = new System.Drawing.Size(134, 100);
            this.groupBoxStrategy.TabIndex = 4;
            this.groupBoxStrategy.TabStop = false;
            this.groupBoxStrategy.Text = "Метод аналізу";

            this.rbLinq.AutoSize = true;
            this.rbLinq.Location = new System.Drawing.Point(7, 68);
            this.rbLinq.Name = "rbLinq";
            this.rbLinq.Size = new System.Drawing.Size(87, 17);
            this.rbLinq.TabIndex = 2;
            this.rbLinq.TabStop = true;
            this.rbLinq.Text = "LINQ to XML";
            this.rbLinq.UseVisualStyleBackColor = true;

            this.rbSax.AutoSize = true;
            this.rbSax.Location = new System.Drawing.Point(7, 44);
            this.rbSax.Name = "rbSax";
            this.rbSax.Size = new System.Drawing.Size(66, 17);
            this.rbSax.TabIndex = 1;
            this.rbSax.TabStop = true;
            this.rbSax.Text = "SAX API";
            this.rbSax.UseVisualStyleBackColor = true;

            this.rbDom.AutoSize = true;
            this.rbDom.Checked = true;
            this.rbDom.Location = new System.Drawing.Point(7, 20);
            this.rbDom.Name = "rbDom";
            this.rbDom.Size = new System.Drawing.Size(69, 17);
            this.rbDom.TabIndex = 0;
            this.rbDom.TabStop = true;
            this.rbDom.Text = "DOM API";
            this.rbDom.UseVisualStyleBackColor = true;

            this.groupBoxFilters.Controls.Add(this.label2);
            this.groupBoxFilters.Controls.Add(this.txtSubjectFilter);
            this.groupBoxFilters.Controls.Add(this.label1);
            this.groupBoxFilters.Controls.Add(this.txtNameFilter);
            this.groupBoxFilters.Location = new System.Drawing.Point(162, 80);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new System.Drawing.Size(310, 100);
            this.groupBoxFilters.TabIndex = 5;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Критерії пошуку";

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Предмет:";

            this.txtSubjectFilter.Location = new System.Drawing.Point(76, 47);
            this.txtSubjectFilter.Name = "txtSubjectFilter";
            this.txtSubjectFilter.Size = new System.Drawing.Size(214, 20);
            this.txtSubjectFilter.TabIndex = 2;

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ім\'я:";

            this.txtNameFilter.Location = new System.Drawing.Point(76, 21);
            this.txtNameFilter.Name = "txtNameFilter";
            this.txtNameFilter.Size = new System.Drawing.Size(214, 20);
            this.txtNameFilter.TabIndex = 0;

            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSearch.Location = new System.Drawing.Point(12, 196);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(134, 34);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "ПОШУК";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.btnTransform.Location = new System.Drawing.Point(162, 196);
            this.btnTransform.Name = "btnTransform";
            this.btnTransform.Size = new System.Drawing.Size(310, 34);
            this.btnTransform.TabIndex = 7;
            this.btnTransform.Text = "Конвертувати в HTML (XSLT)";
            this.btnTransform.UseVisualStyleBackColor = true;
            this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);

            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.Location = new System.Drawing.Point(12, 246);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.Size = new System.Drawing.Size(460, 199);
            this.listBoxResults.TabIndex = 8;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.listBoxResults);
            this.Controls.Add(this.btnTransform);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBoxFilters);
            this.Controls.Add(this.groupBoxStrategy);
            this.Controls.Add(this.txtXslPath);
            this.Controls.Add(this.btnLoadXsl);
            this.Controls.Add(this.txtXmlPath);
            this.Controls.Add(this.btnLoadXml);
            this.Name = "MainForm";
            this.Text = "Лабораторна 2 - Варіант 6";
            this.groupBoxStrategy.ResumeLayout(false);
            this.groupBoxStrategy.PerformLayout();
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxFilters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadXml;
        private System.Windows.Forms.TextBox txtXmlPath;
        private System.Windows.Forms.Button btnLoadXsl;
        private System.Windows.Forms.TextBox txtXslPath;
        private System.Windows.Forms.GroupBox groupBoxStrategy;
        private System.Windows.Forms.RadioButton rbLinq;
        private System.Windows.Forms.RadioButton rbSax;
        private System.Windows.Forms.RadioButton rbDom;
        private System.Windows.Forms.GroupBox groupBoxFilters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSubjectFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnTransform;
        private System.Windows.Forms.ListBox listBoxResults;
    }
}