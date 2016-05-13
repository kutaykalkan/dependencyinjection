

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt SubledgerData table
	/// </summary>
	[Serializable]
	public abstract class SubledgerDataInfoBase
	{

		protected System.Int64? _AccountID = 0;
		protected System.String _BaseCCY = "";
		protected System.Int64? _GLDataID = 0;
		protected System.String _ReportingCCY = "";
		protected System.Decimal? _SubledgerBalanceBaseCCY = 0.00M;
		protected System.Decimal? _SubledgerBalanceReportingCCY = 0.00M;
		protected System.Int32? _SubledgerDataID = 0;

        protected System.Int32? _DataImportID = 0;
        protected System.Int32? _ReconciliationPeriodID = 0;

		public bool IsAccountIDNull = true;


		public bool IsBaseCCYNull = true;


		public bool IsGLDataIDNull = true;


		public bool IsReportingCCYNull = true;


		public bool IsSubledgerBalanceBaseCCYNull = true;


		public bool IsSubledgerBalanceReportingCCYNull = true;


		public bool IsSubledgerDataIDNull = true;


        public bool IsDataImportIDNull = true;


        public bool IsReconciliationPeriodIDNull = true;






		[XmlElement(ElementName = "AccountID")]
		public virtual System.Int64? AccountID 
		{
			get 
			{
				return this._AccountID;
			}
			set 
			{
				this._AccountID = value;

									this.IsAccountIDNull = false;
							}
		}			

		[XmlElement(ElementName = "BaseCCY")]
		public virtual System.String BaseCCY 
		{
			get 
			{
				return this._BaseCCY;
			}
			set 
			{
				this._BaseCCY = value;

									this.IsBaseCCYNull = (_BaseCCY == null);
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

		[XmlElement(ElementName = "ReportingCCY")]
		public virtual System.String ReportingCCY 
		{
			get 
			{
				return this._ReportingCCY;
			}
			set 
			{
				this._ReportingCCY = value;

									this.IsReportingCCYNull = (_ReportingCCY == null);
							}
		}			

		[XmlElement(ElementName = "SubledgerBalanceBaseCCY")]
		public virtual System.Decimal? SubledgerBalanceBaseCCY 
		{
			get 
			{
				return this._SubledgerBalanceBaseCCY;
			}
			set 
			{
				this._SubledgerBalanceBaseCCY = value;

									this.IsSubledgerBalanceBaseCCYNull = false;
							}
		}			

		[XmlElement(ElementName = "SubledgerBalanceReportingCCY")]
		public virtual System.Decimal? SubledgerBalanceReportingCCY 
		{
			get 
			{
				return this._SubledgerBalanceReportingCCY;
			}
			set 
			{
				this._SubledgerBalanceReportingCCY = value;

									this.IsSubledgerBalanceReportingCCYNull = false;
							}
		}			

		[XmlElement(ElementName = "SubledgerDataID")]
		public virtual System.Int32? SubledgerDataID 
		{
			get 
			{
				return this._SubledgerDataID;
			}
			set 
			{
				this._SubledgerDataID = value;

									this.IsSubledgerDataIDNull = false;
							}
		}



        [XmlElement(ElementName = "DataImportID")]
        public virtual System.Int32? DataImportID
        {
            get
            {
                return this._DataImportID;
            }
            set
            {
                this._DataImportID = value;

                this.IsDataImportIDNull = false;
            }
        }


        [XmlElement(ElementName = "ReconciliationPeriodID")]
        public virtual System.Int32? ReconciliationPeriodID
        {
            get
            {
                return this._ReconciliationPeriodID;
            }
            set
            {
                this._ReconciliationPeriodID = value;

                this.IsReconciliationPeriodIDNull = false;
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
