using System;
using System.Data;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;

namespace PDSA.WPF
{
  /// <summary>
  /// This class has method to help you work with the WPF ListView control
  /// </summary>
  public class PDSAWPFListView
  {
    #region CreateGridViewColumns Method for Type
    /// <summary>
    /// Used to build the &lt;GridView&gt; and the &lt;GridViewColumn&gt; controls for a WPF ListView. 
    /// This is used when you want to build a ListView of columns using Reflection of a Type
    /// NOTE: You can't use this routine when you have bound the ListView to an ObjectDataProvider.
    /// </summary>
    /// <example>
    ///  lstXML.View = PDSAWPFListView.CreateGridViewColumns(typeOf(Product));
    ///  lstXML.DataContext = dt;
    /// </example>
    /// <param name="anyType">A Type to extract properties from</param>
    /// <returns>A GridView Object</returns>
    public static GridView CreateGridViewColumns(Type anyType)
    {
      // Create the GridView
      GridView gv = new GridView();
      GridViewColumn col;

      // Get the public properties.
      PropertyInfo[] propInfo = anyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

      foreach (PropertyInfo item in propInfo)
      {
        col = new GridViewColumn();
        col.DisplayMemberBinding = new Binding(item.Name);
        col.Header = item.Name;
        col.Width = Double.NaN;
        gv.Columns.Add(col);
      }

      return gv;
    }
    #endregion

    #region CreateGridViewColumns Method for DataTable
    /// <summary>
    /// Used to build the &lt;GridView&gt; and the &lt;GridViewColumn&gt; controls for a WPF ListView. 
    /// This is used when you want to build a ListView of columns from a DataTable object.
    /// NOTE: You can't use this routine when you have bound the ListView to an ObjectDataProvider.
    /// </summary>
    /// <example>
    ///  DataTable dt;
    ///  
    ///  dt = PDSAXML.XmlFileToDataTable(txtFileToDataset.Text);
    ///  
    ///  lstXML.View = PDSAWPFListView.CreateGridViewColumns(dt);
    ///  lstXML.DataContext = dt;
    /// </example>
    /// <param name="dt">A Data Table</param>
    /// <returns>A GridView object</returns>
    public static GridView CreateGridViewColumns(DataTable dt)
    {
      // Create the GridView
      GridView gv = new GridView();
      gv.AllowsColumnReorder = true;

      return CreateGridViewColumns(gv, dt);
    }

    /// <summary>
    /// Used to build the &lt;GridView&gt; and the &lt;GridViewColumn&gt; controls for a WPF ListView. 
    /// This is used when you want to build a ListView of columns from a DataTable object.
    /// NOTE: You can't use this routine when you have bound the ListView to an ObjectDataProvider.
    /// </summary>
    /// <example>
    ///  DataTable dt;
    ///  
    ///  dt = PDSAXML.XmlFileToDataTable(txtFileToDataset.Text);
    ///  
    ///  lstXML.View = PDSAWPFListView.CreateGridViewColumns(dt);
    ///  lstXML.DataContext = dt;
    /// </example>
    /// <param name="gv">A GridView Object</param>
    /// <param name="dt">A Data Table</param>
    /// <returns>A GridView object</returns>
    public static GridView CreateGridViewColumns(GridView gv, DataTable dt)
    {
      GridViewColumn col;

      foreach (DataColumn item in dt.Columns)
      {
        col = new GridViewColumn();
        col.DisplayMemberBinding = new Binding(item.ColumnName);
        col.Header = item.ColumnName;
        col.Width = Double.NaN;
        gv.Columns.Add(col);
      }

      return gv;
    }
    #endregion
  }
}