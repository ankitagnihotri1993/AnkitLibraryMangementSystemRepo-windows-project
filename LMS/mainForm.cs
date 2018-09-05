using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void studentTSM_Click(object sender, EventArgs e)
        {
            studentForm std = new studentForm();
            std.StartPosition = FormStartPosition.CenterScreen;
            std.ShowDialog();
           
        }

        private void teacherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TeacherForm teacher = new TeacherForm();
            teacher.StartPosition = FormStartPosition.CenterScreen;
            teacher.ShowDialog();
        }

        private void guestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guestForm guest = new guestForm();
            guest.StartPosition = FormStartPosition.CenterScreen;
            guest.ShowDialog();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserForm user = new UserForm();
            user.StartPosition = FormStartPosition.CenterScreen;
            user.ShowDialog();
        }
    }
}
