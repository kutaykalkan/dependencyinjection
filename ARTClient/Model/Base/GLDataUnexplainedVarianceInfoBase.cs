

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataUnexplainedVariance table
    /// </summary>
    [Serializable]
    public abstract class GLDataUnexplainedVarianceInfoBase
    {
        protected System.DateTime? _CommentDate = null;
        protected System.String _Comments = "";
        protected System.Int64? _GLDataID = null;
        protected System.Int64? _GLDataUnexplainedVarianceID = null;

        public bool IsCommentDateNull = true;


        public bool IsCommentsNull = true;


        public bool IsGLDataIDNull = true;


        public bool IsGLDataUnexplainedVarianceIDNull = true;


        [XmlElement(ElementName = "CommentDate")]
        public virtual System.DateTime? CommentDate
        {
            get
            {
                return this._CommentDate;
            }
            set
            {
                this._CommentDate = value;

                this.IsCommentDateNull = (_CommentDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "Comments")]
        public virtual System.String Comments
        {
            get
            {
                return this._Comments;
            }
            set
            {
                this._Comments = value;

                this.IsCommentsNull = (_Comments == null);
            }
        }

        [XmlElement(ElementName = "GLDataID")]
        public virtual System.Int64? GLDataID
        {
            get
            {
                return this._GLDataID;
            }
            set
            {
                this._GLDataID = value;

                this.IsGLDataIDNull = false;
            }
        }

        [XmlElement(ElementName = "GLDataUnexplainedVarianceID")]
        public virtual System.Int64? GLDataUnexplainedVarianceID
        {
            get
            {
                return this._GLDataUnexplainedVarianceID;
            }
            set
            {
                this._GLDataUnexplainedVarianceID = value;

                this.IsGLDataUnexplainedVarianceIDNull = false;
            }
        }

        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }

    }
}
