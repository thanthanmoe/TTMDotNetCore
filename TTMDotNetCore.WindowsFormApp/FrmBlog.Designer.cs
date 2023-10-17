namespace TTMDotNetCore.WindowsFormApp
{ 
    partial class FrmBlog
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
            this.components = new System.ComponentModel.Container();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.aHMTZDotNetCoreDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.aHMTZDotNetCoreDataSet = new TTMDotNetCore.WindowsFormApp.AHMTZDotNetCoreDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.aHMTZDotNetCoreDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aHMTZDotNetCoreDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(17, 245);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(205, 72);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(18, 113);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(5);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(139, 26);
            this.txtTitle.TabIndex = 1;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(18, 158);
            this.txtAuthor.Margin = new System.Windows.Forms.Padding(5);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(139, 26);
            this.txtAuthor.TabIndex = 2;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(18, 202);
            this.txtContent.Margin = new System.Windows.Forms.Padding(5);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(139, 26);
            this.txtContent.TabIndex = 3;
            // 
            // aHMTZDotNetCoreDataSetBindingSource
            // 
            this.aHMTZDotNetCoreDataSetBindingSource.DataSource = this.aHMTZDotNetCoreDataSet;
            this.aHMTZDotNetCoreDataSetBindingSource.Position = 0;
            // 
            // aHMTZDotNetCoreDataSet
            // 
            this.aHMTZDotNetCoreDataSet.DataSetName = "AHMTZDotNetCoreDataSet";
            this.aHMTZDotNetCoreDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FrmBlog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 624);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmBlog";
            this.Text = "Form1";
            this.Click += new System.EventHandler(this.btnSave_Click);
            ((System.ComponentModel.ISupportInitialize)(this.aHMTZDotNetCoreDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aHMTZDotNetCoreDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.TextBox txtTitle;
    private System.Windows.Forms.TextBox txtAuthor;
    private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.BindingSource aHMTZDotNetCoreDataSetBindingSource;
        private AHMTZDotNetCoreDataSet aHMTZDotNetCoreDataSet;
    }
}

