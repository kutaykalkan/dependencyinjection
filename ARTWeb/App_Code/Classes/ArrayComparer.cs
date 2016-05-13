using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Web.UI;

namespace SkyStem.ART.Web.Utility
{
    #region Array Comparer

    internal class ArrayComparer : IComparer
    {

        protected string[] m_property;
        protected SortDirection[] m_SortOrder;

        // '' -----------------------------------------------------------------------------
        // '' 
        // '' Constructor
        // '' 
        // '' Property name on the basis of which the objects are to be compared
        // '' 
        // '' 
        // '' -----------------------------------------------------------------------------
        public ArrayComparer(string[] propertyValue)
        {
            m_property = propertyValue;
            SortDirection[] _sortDirection = new SortDirection[1];
            _sortDirection[0] = SortDirection.Ascending;
            m_SortOrder = _sortDirection;
        }

        // '' -----------------------------------------------------------------------------
        // '' 
        // '' Constructor
        // '' 
        // '' Property name on the basis of which the objects are to be compared
        // '' 
        // '' 
        // '' -----------------------------------------------------------------------------
        public ArrayComparer(string[] propertyValue, SortDirection[] sortOrder)
        {
            m_property = propertyValue;
            m_SortOrder = sortOrder;
        }

        // '' -----------------------------------------------------------------------------
        // '' 
        // '' Function to Compare the Values of the Objects for the specified property
        // '' 
        // '' Object 1
        // '' Object 2, the object to whom comparison is to be done
        // '' 
        // '' 
        // '' 
        // '' -----------------------------------------------------------------------------
        public Int32 Compare(object obj1, object obj2)
        {
            int propertyIndex = 0;
            if (!obj1.GetType().Equals(obj2.GetType()))
            {
                throw new ArgumentException("Passed objects are not of the same type");
            }
            int result = CompareValue(obj1, obj2, propertyIndex);
            return Flip(result, propertyIndex);
        }

        // '' -----------------------------------------------------------------------------
        // '' 
        // '' Function to Get the Value of the Property
        // '' Keeping in mind, the proprty might be of nested Objects 
        // '' 
        // '' Object for which the value is to be determined
        // '' Name of the property whose value is to be determined
        // '' 
        // '' 
        // '' 
        // '' -----------------------------------------------------------------------------
        public static object GetPropValue(object obj, string propertyValue)
        {
            string displyValueFunctionFQN = "";
            //  The property might not directly give the value for Sorting purposes. e.g. Buy Order Status. 
            //  In this case the property will give Status Codes like 100, 200.  
            //  While the actual sorting is to be done on displayed value (Status Description) like "waiting for pre-confirmation"
            if ((propertyValue.IndexOf("|") != -1))
            {
                //  The Function that will give the displayed value, this also contains the name of Class
                displyValueFunctionFQN = propertyValue.Split('|')[1];
                //  Actual Property Name
                propertyValue = propertyValue.Split('|')[0];
            }
            object objectCopy = obj;
            string[] toComp = propertyValue.Split('.');
            Type objType = obj.GetType();
            Int16 i = 0;
            while ((i < toComp.Length))
            {
                PropertyInfo prop = objType.GetProperty(toComp[i]);
                if (!(prop == null))
                {
                    objectCopy = prop.GetValue(objectCopy, null);
                }
                else
                {
                    FieldInfo fi = objType.GetField(toComp[i]);
                    if (!(fi == null))
                    {
                        objectCopy = fi.GetValue(objectCopy);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid property specified");
                    }
                }
                if (!(objectCopy == null))
                {
                    objType = objectCopy.GetType();
                }
                else
                {
                    return null;
                }
                i++;
            }
            //  If the Function that gives Displayed value is present, means we need to get the disaplyed value
            //  of the property for the Sorting purposes
            if ((displyValueFunctionFQN != ""))
            {
                Int16 dotPos;
                string displyValueClassName;
                string displyValueFunctionName;
                object objDisplayValueFunctionClass;
                dotPos = (short)displyValueFunctionFQN.LastIndexOf('.');
                //  Get the Class to which the Display Function belongs to
                displyValueClassName = displyValueFunctionFQN.Substring(0, dotPos);
                objDisplayValueFunctionClass = Activator.CreateInstance(Type.GetType(displyValueClassName));
                //  Get the Display Value's Function 
                displyValueFunctionName = displyValueFunctionFQN.Substring((dotPos + 1));
                MethodInfo objMethod = objDisplayValueFunctionClass.GetType().GetMethod(displyValueFunctionName);
                //  Invoke the Display Value function, and get the actual displayed value
                objectCopy = objMethod.Invoke(objDisplayValueFunctionClass, new object[] {
																							 objectCopy});
            }
            return objectCopy;
        }

        // '' -----------------------------------------------------------------------------
        // '' 
        // '' Function to return the Result of Comparison
        // '' 
        // '' Object 1
        // '' Object 2, the object to whom comparison is to be done
        // '' Index no. for the property
        // '' 
        // '' 
        // '' 
        // '' -----------------------------------------------------------------------------
        private Int32 CompareValue(object obj1, object obj2, int propertyIndex)
        {
            // compare
            object val1 = GetPropValue(obj1, m_property[propertyIndex]);
            object val2 = GetPropValue(obj2, m_property[propertyIndex]);
            if (((val1 == null)
                && (val2 == null)))
            {
                return 0;
            }
            else if (((val1 == null)
                && !(val2 == null)))
            {
                return -1;
            }
            else if ((!(val1 == null)
                && (val2 == null)))
            {
                return 1;
            }
            int CompareResult;
            if ((val1 is IComparable))
            {
                IComparable oComparer = (IComparable)val1;
                //  Check again
                CompareResult = oComparer.CompareTo(val2);
                if ((CompareResult == 0) && ((propertyIndex + 1) < m_property.Length))
                {
                    return CompareValue(obj1, obj2, (propertyIndex + 1));
                }
                else
                {
                    return CompareResult;
                }
            }
            else
            {
                throw new ArgumentException("The property requested must implement IComparable");
            }
        }

        // '' -----------------------------------------------------------------------------
        // '' 
        // '' Function to get the DataType of the property field
        // '' 
        // '' Object for which the value is to be determined
        // '' Name of the property whose Type is to be determined
        // '' 
        // '' 
        // '' 
        // '' -----------------------------------------------------------------------------
        public static System.Type GetPropType(object obj, string propertyValue)
        {
            object objectCopy = obj;
            string[] toComp = propertyValue.Split('.');
            Type objType = obj.GetType();
            Int16 i = 0;
            while ((i < toComp.Length))
            {
                PropertyInfo prop = objType.GetProperty(toComp[i]);
                if (!(prop == null))
                {
                    objectCopy = prop.GetValue(objectCopy, null);
                }
                else
                {
                    FieldInfo fi = objType.GetField(toComp[i]);
                    if (!(fi == null))
                    {
                        objectCopy = fi.GetValue(objectCopy);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid property specified");
                    }
                }
                objType = objectCopy.GetType();
                i++;
            }
            return objType;
        }

        private int Flip(int i, int propertyIndex)
        {
            int sortOrder;
            sortOrder = m_SortOrder[propertyIndex] == SortDirection.Ascending ? 1 : -1;
            return (i * sortOrder);

        }
    }

    #endregion
}