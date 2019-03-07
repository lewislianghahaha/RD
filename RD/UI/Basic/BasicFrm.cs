using System;
using System.Windows.Forms;

namespace RD.UI.Basic
{
    public partial class BasicFrm : Form
    {
        public BasicFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
            Show();
        }

        private void OnRegisterEvents()
        {
            btnSearch.Click += BtnSearch_Click;
            btnCreate.Click += BtnCreate_Click;
            btnChange.Click += BtnChange_Click;
            btnDel.Click += BtnDel_Click;
        }

        private void Show()
        {
            //第一层
            var anime = new TreeNode("全部");
            //第二层
            //anime.Nodes.Add("国创");
            //anime.Nodes.Add("一人之下");
            //anime.Nodes.Add("狐妖小红娘");
            tview.Tag = 1;
            tview.Text = "";
            tview.Nodes.Add(anime);
        }

        /// <summary>
        /// 新建分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode treeNode=new TreeNode();
                treeNode.Tag = 1;
                treeNode.Text = "hahaha";
                //tview.Nodes.Add(treeNode);
                tview.SelectedNode.Nodes.Add(treeNode); //对所选择的节点增加子节点
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 编辑分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChange_Click(object sender, EventArgs e)
        {
            try
            {
                var a = (int) tview.SelectedNode.Tag;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                tview.SelectedNode.Remove();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 查询功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
