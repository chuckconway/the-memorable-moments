using System.Collections.Generic;

namespace TheMemorableMoments.UI.Web.Controls.TreeView
{
    public class JsTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsTreeNode"/> class.
        /// </summary>
        public JsTreeNode()
        {
            attr = new Attributes();
            data = new Data();
            children = new List<JsTreeNode>();
        }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>The attributes.</value>
        public Attributes attr { get; set; }


        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public Data data { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string state { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public List<JsTreeNode> children { get; set; }
    }
}