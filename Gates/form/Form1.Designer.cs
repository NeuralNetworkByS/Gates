namespace Gates
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.XORRB = new System.Windows.Forms.RadioButton();
            this.ANDRB = new System.Windows.Forms.RadioButton();
            this.ORRB = new System.Windows.Forms.RadioButton();
            this.GateTrainingValuesView = new System.Windows.Forms.DataGridView();
            this.DataTraningView = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SigmoidFunctionRB = new System.Windows.Forms.RadioButton();
            this.JumpFunctionRB = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LearningRateTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TwoNeuronsTB = new System.Windows.Forms.RadioButton();
            this.OneNeuronRB = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.TrainingLogLB = new System.Windows.Forms.ListBox();
            this.TrainingStartButton = new System.Windows.Forms.Button();
            this.ChartButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GateTrainingValuesView)).BeginInit();
            this.DataTraningView.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.XORRB);
            this.groupBox1.Controls.Add(this.ANDRB);
            this.groupBox1.Controls.Add(this.ORRB);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Typy bramek";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // XORRB
            // 
            this.XORRB.AutoSize = true;
            this.XORRB.Location = new System.Drawing.Point(23, 89);
            this.XORRB.Name = "XORRB";
            this.XORRB.Size = new System.Drawing.Size(59, 21);
            this.XORRB.TabIndex = 3;
            this.XORRB.TabStop = true;
            this.XORRB.Text = "XOR";
            this.XORRB.UseVisualStyleBackColor = true;
            this.XORRB.CheckedChanged += new System.EventHandler(this.XORRB_CheckedChanged);
            // 
            // ANDRB
            // 
            this.ANDRB.AutoSize = true;
            this.ANDRB.Location = new System.Drawing.Point(23, 62);
            this.ANDRB.Name = "ANDRB";
            this.ANDRB.Size = new System.Drawing.Size(58, 21);
            this.ANDRB.TabIndex = 2;
            this.ANDRB.TabStop = true;
            this.ANDRB.Text = "AND";
            this.ANDRB.UseVisualStyleBackColor = true;
            this.ANDRB.CheckedChanged += new System.EventHandler(this.ANDRB_CheckedChanged);
            // 
            // ORRB
            // 
            this.ORRB.AutoSize = true;
            this.ORRB.Checked = true;
            this.ORRB.Location = new System.Drawing.Point(23, 35);
            this.ORRB.Name = "ORRB";
            this.ORRB.Size = new System.Drawing.Size(50, 21);
            this.ORRB.TabIndex = 1;
            this.ORRB.TabStop = true;
            this.ORRB.Text = "OR";
            this.ORRB.UseVisualStyleBackColor = true;
            this.ORRB.CheckedChanged += new System.EventHandler(this.ORRB_CheckedChanged);
            // 
            // GateTrainingValuesView
            // 
            this.GateTrainingValuesView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GateTrainingValuesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GateTrainingValuesView.Location = new System.Drawing.Point(16, 39);
            this.GateTrainingValuesView.Name = "GateTrainingValuesView";
            this.GateTrainingValuesView.RowTemplate.Height = 24;
            this.GateTrainingValuesView.Size = new System.Drawing.Size(143, 202);
            this.GateTrainingValuesView.TabIndex = 1;
            this.GateTrainingValuesView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // DataTraningView
            // 
            this.DataTraningView.Controls.Add(this.GateTrainingValuesView);
            this.DataTraningView.Location = new System.Drawing.Point(351, 12);
            this.DataTraningView.Name = "DataTraningView";
            this.DataTraningView.Size = new System.Drawing.Size(174, 266);
            this.DataTraningView.TabIndex = 4;
            this.DataTraningView.TabStop = false;
            this.DataTraningView.Text = "OR";
            this.DataTraningView.Enter += new System.EventHandler(this.DataTraningView_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SigmoidFunctionRB);
            this.groupBox2.Controls.Add(this.JumpFunctionRB);
            this.groupBox2.Location = new System.Drawing.Point(12, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 132);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pobudzenie";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // SigmoidFunctionRB
            // 
            this.SigmoidFunctionRB.AutoSize = true;
            this.SigmoidFunctionRB.Location = new System.Drawing.Point(23, 76);
            this.SigmoidFunctionRB.Name = "SigmoidFunctionRB";
            this.SigmoidFunctionRB.Size = new System.Drawing.Size(87, 21);
            this.SigmoidFunctionRB.TabIndex = 3;
            this.SigmoidFunctionRB.Text = "Sigmoida";
            this.SigmoidFunctionRB.UseVisualStyleBackColor = true;
            this.SigmoidFunctionRB.CheckedChanged += new System.EventHandler(this.SigmoidFunctionRB_CheckedChanged);
            // 
            // JumpFunctionRB
            // 
            this.JumpFunctionRB.AutoSize = true;
            this.JumpFunctionRB.Checked = true;
            this.JumpFunctionRB.Location = new System.Drawing.Point(23, 49);
            this.JumpFunctionRB.Name = "JumpFunctionRB";
            this.JumpFunctionRB.Size = new System.Drawing.Size(85, 21);
            this.JumpFunctionRB.TabIndex = 2;
            this.JumpFunctionRB.TabStop = true;
            this.JumpFunctionRB.Text = "Skokowe";
            this.JumpFunctionRB.UseVisualStyleBackColor = true;
            this.JumpFunctionRB.CheckedChanged += new System.EventHandler(this.JumpFunctionRB_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LearningRateTB);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(186, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(147, 83);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dodatkowe Parametry";
            // 
            // LearningRateTB
            // 
            this.LearningRateTB.Location = new System.Drawing.Point(54, 47);
            this.LearningRateTB.Name = "LearningRateTB";
            this.LearningRateTB.Size = new System.Drawing.Size(74, 22);
            this.LearningRateTB.TabIndex = 1;
            this.LearningRateTB.Text = "0.5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "wsu";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TwoNeuronsTB);
            this.groupBox4.Controls.Add(this.OneNeuronRB);
            this.groupBox4.Location = new System.Drawing.Point(186, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(147, 98);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ilość neurnów";
            // 
            // TwoNeuronsTB
            // 
            this.TwoNeuronsTB.AutoSize = true;
            this.TwoNeuronsTB.Location = new System.Drawing.Point(19, 66);
            this.TwoNeuronsTB.Name = "TwoNeuronsTB";
            this.TwoNeuronsTB.Size = new System.Drawing.Size(37, 21);
            this.TwoNeuronsTB.TabIndex = 3;
            this.TwoNeuronsTB.Text = "2";
            this.TwoNeuronsTB.UseVisualStyleBackColor = true;
            this.TwoNeuronsTB.CheckedChanged += new System.EventHandler(this.TwoNeuronsTB_CheckedChanged);
            // 
            // OneNeuronRB
            // 
            this.OneNeuronRB.AutoSize = true;
            this.OneNeuronRB.Checked = true;
            this.OneNeuronRB.Location = new System.Drawing.Point(19, 39);
            this.OneNeuronRB.Name = "OneNeuronRB";
            this.OneNeuronRB.Size = new System.Drawing.Size(37, 21);
            this.OneNeuronRB.TabIndex = 2;
            this.OneNeuronRB.TabStop = true;
            this.OneNeuronRB.Text = "1";
            this.OneNeuronRB.UseVisualStyleBackColor = true;
            this.OneNeuronRB.CheckedChanged += new System.EventHandler(this.OneNeuronRB_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.TrainingLogLB);
            this.groupBox5.Location = new System.Drawing.Point(12, 290);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(513, 251);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Raport Treningu";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(174, -29);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 30);
            this.button2.TabIndex = 9;
            this.button2.Text = "Trenuj";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // TrainingLogLB
            // 
            this.TrainingLogLB.FormattingEnabled = true;
            this.TrainingLogLB.ItemHeight = 16;
            this.TrainingLogLB.Location = new System.Drawing.Point(23, 38);
            this.TrainingLogLB.Name = "TrainingLogLB";
            this.TrainingLogLB.Size = new System.Drawing.Size(475, 196);
            this.TrainingLogLB.TabIndex = 0;
            // 
            // TrainingStartButton
            // 
            this.TrainingStartButton.Location = new System.Drawing.Point(186, 212);
            this.TrainingStartButton.Name = "TrainingStartButton";
            this.TrainingStartButton.Size = new System.Drawing.Size(147, 30);
            this.TrainingStartButton.TabIndex = 8;
            this.TrainingStartButton.Text = "Trenuj";
            this.TrainingStartButton.UseVisualStyleBackColor = true;
            this.TrainingStartButton.Click += new System.EventHandler(this.TrainingStartButton_Click);
            // 
            // ChartButton
            // 
            this.ChartButton.Location = new System.Drawing.Point(186, 248);
            this.ChartButton.Name = "ChartButton";
            this.ChartButton.Size = new System.Drawing.Size(147, 30);
            this.ChartButton.TabIndex = 9;
            this.ChartButton.Text = "Wykres";
            this.ChartButton.UseVisualStyleBackColor = true;
            this.ChartButton.Click += new System.EventHandler(this.ChartButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 553);
            this.Controls.Add(this.ChartButton);
            this.Controls.Add(this.TrainingStartButton);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.DataTraningView);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Bramki";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GateTrainingValuesView)).EndInit();
            this.DataTraningView.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton XORRB;
        private System.Windows.Forms.RadioButton ANDRB;
        private System.Windows.Forms.RadioButton ORRB;
        private System.Windows.Forms.DataGridView GateTrainingValuesView;
        private System.Windows.Forms.GroupBox DataTraningView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton SigmoidFunctionRB;
        private System.Windows.Forms.RadioButton JumpFunctionRB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox LearningRateTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton TwoNeuronsTB;
        private System.Windows.Forms.RadioButton OneNeuronRB;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox TrainingLogLB;
        private System.Windows.Forms.Button TrainingStartButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button ChartButton;
    }
}

