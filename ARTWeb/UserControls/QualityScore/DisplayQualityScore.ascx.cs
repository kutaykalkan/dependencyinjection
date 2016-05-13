using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using System.Drawing;

namespace SkyStem.ART.Web.UserControls
{
    public partial class DisplayQualityScore : UserControlQualityScoreBase
    {
        public void SetGLDataQualityScoreCount(Dictionary<ARTEnums.QualityScoreType, int?> dictGLDataQualityScoreCount)
        {
            if (dictGLDataQualityScoreCount != null && dictGLDataQualityScoreCount.ContainsKey(ARTEnums.QualityScoreType.SystemScore))
            {
                int? score = dictGLDataQualityScoreCount[ARTEnums.QualityScoreType.SystemScore];
                int threshold = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.QUALITY_SCORE_THRESHOLD));
                if (score > threshold)
                    lblQualityScoreValue.ForeColor = Color.Red;
                lblQualityScoreValue.Text = Helper.GetDisplayIntegerValue(score);
            }
        }
    }
}