


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class GLDataReviewNoteDetailDAO : GLDataReviewNoteDetailDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataReviewNoteDetailDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
    }
}