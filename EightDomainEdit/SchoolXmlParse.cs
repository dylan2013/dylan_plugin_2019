using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Z_EightDomainEdit
{
    public partial class SchoolXmlParse : BaseForm
    {
        public SchoolXmlParse(DataGridViewRow row)
        {
            InitializeComponent();

            XElement xml1 = new XElement(XElement.Parse("" + row.Cells[3].Value));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml1.ToString());
            ConvertXmlNodeToTreeNode(doc, treeXml1.Nodes);

            XElement xml2 = new XElement(XElement.Parse("" + row.Cells[4].Value));
            XmlDocument doc2 = new XmlDocument();
            doc2.LoadXml(xml2.ToString());
            ConvertXmlNodeToTreeNode(doc2, treeXml2.Nodes);


        }

        private void ConvertXmlNodeToTreeNode(XmlNode xmlNode,
      TreeNodeCollection treeNodes)
        {
            TreeNode newTreeNode = treeNodes.Add(xmlNode.Name);
            if (xmlNode.Name == "#document")
            {
                newTreeNode.ExpandAll();
            }
            else if(xmlNode.Name == "Configurations")
            {
                newTreeNode.ExpandAll();
            }
            else if (xmlNode.Name == "Configuration")
            {
                newTreeNode.ExpandAll();
            }
            else if (xmlNode.Name == "#text")
            {
                newTreeNode.ExpandAll();
            }

            switch (xmlNode.NodeType)
            {
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.XmlDeclaration:
                    newTreeNode.Text = "<?" + xmlNode.Name + " " + xmlNode.Value + "?>";
                    break;
                case XmlNodeType.Element:
                    {
                        if (xmlNode.Name == "Subjects")
                        {
                            newTreeNode.Text = "<科目清單>";
                        }
                        else if (xmlNode.Name == "Subject")
                        {
                            newTreeNode.Text = "<科目>";
                        }
                        else if (xmlNode.Name == "Domains")
                        {
                            newTreeNode.Text = "<領域清單>";
                            newTreeNode.ExpandAll();
                        }
                        else if (xmlNode.Name == "Domain")
                        {
                            newTreeNode.Text = "<領域>";
                            newTreeNode.ExpandAll();
                        }
                        else
                        {
                            newTreeNode.Text = "<" + xmlNode.Name + ">";
                        }
                    }
                    break;
                case XmlNodeType.Attribute:
                    newTreeNode.Text = "屬性：" + xmlNode.Name;
                    newTreeNode.ExpandAll();
                    break;
                case XmlNodeType.Text:
                case XmlNodeType.CDATA:
                    {
                        {
                            if (xmlNode.Value.Length > 50)
                            {
                                XElement xml2 = new XElement(XElement.Parse(xmlNode.Value));
                                XmlDocument doc2 = new XmlDocument();
                                doc2.LoadXml(xml2.ToString());
                                ConvertXmlNodeToTreeNode(doc2, treeNodes);
                                newTreeNode.ExpandAll();
                            }
                            else
                            {
                                if (xmlNode.Value == "SubjectOrdinal")
                                {
                                    newTreeNode.Text = "科目對照表";
                                }
                                else if (xmlNode.Value == "DomainOrdinal")
                                {
                                    newTreeNode.Text = "領域對照表";
                                    newTreeNode.ExpandAll();
                                }
                                else
                                {
                                    if (xmlNode.Value == "")
                                    {
                                        newTreeNode.Text = "（無資料）";
                                    }
                                    else if(xmlNode.Value == "科技")
                                    {
                                        newTreeNode.Text = xmlNode.Value;
                                        newTreeNode.ForeColor = Color.Red;
                                    }
                                    else if (xmlNode.Value == "藝術")
                                    {
                                        newTreeNode.Text = xmlNode.Value;
                                        newTreeNode.ForeColor = Color.Red;
                                    }
                                    else if (xmlNode.Value == "自然科學")
                                    {
                                        newTreeNode.Text = xmlNode.Value;
                                        newTreeNode.ForeColor = Color.Red;
                                    }
                                    else
                                    {
                                        newTreeNode.Text = xmlNode.Value;
                                    }
                                }
                            }
                            break;
                        }
                    }
                case XmlNodeType.Comment:
                    newTreeNode.Text = "<!--" + xmlNode.Value + "-->";
                    break;
            }

            if (xmlNode.Attributes != null)
            {
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    ConvertXmlNodeToTreeNode(attribute, newTreeNode.Nodes);
                }
            }

            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                ConvertXmlNodeToTreeNode(childNode, newTreeNode.Nodes);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
