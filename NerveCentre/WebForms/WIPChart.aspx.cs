using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Data.Entity.Core.EntityClient;

namespace NerveCentre
{
    public partial class WIPChart : System.Web.UI.Page
    {
        Common common = new Common();
        double MaxVal = 0;
        double MinVal = 0;
        int YAxisInterval;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["iframe"] == "true")
            {
                MasterPageFile = "~/WebForms/BlankForFrames.Master";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
               
                DataSet dsThresholdValues = new DataSet();
                dsThresholdValues = common.ReturnDataSet("SELECT ParamName, ParamValue FROM     CMEConfigurations WHERE(ParamName = N'MinimumWIPHours') OR (ParamName = N'MaximumWIPHours') ORDER BY ParamName");
                if (dsThresholdValues.Tables[0].Rows.Count > 0)
                {
                    MaxVal = Convert.ToDouble(dsThresholdValues.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    MinVal = Convert.ToDouble(dsThresholdValues.Tables[0].Rows[1].ItemArray.GetValue(1).ToString());
                }

                DataSet dsYAxisSpacing = new DataSet();
                dsYAxisSpacing = common.ReturnDataSet("SELECT ParamName, ParamValue FROM     CMEConfigurations WHERE(ParamName = N'YAxisLabelInterval')");
                if (dsYAxisSpacing.Tables[0].Rows.Count > 0)
                {
                    YAxisInterval = Convert.ToInt32(dsYAxisSpacing.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                }

                DataSet dsFactory = new DataSet();
                dsFactory = common.ReturnDataSet("SELECT ParamValue FROM     CMEConfigurations WHERE(ParamName = N'FactoryName')");
                lblFactory.InnerText = dsFactory.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                StripLine MaxLine = new StripLine();
                MaxLine.BorderColor = System.Drawing.Color.Red;
                MaxLine.BorderWidth = 5;
                MaxLine.IntervalOffset = MaxVal;
                Chart1.ChartAreas["ChartArea1"].AxisY.StripLines.Add(MaxLine);

                StripLine MinLine = new StripLine();
                MinLine.BorderColor = System.Drawing.Color.GreenYellow;
                MinLine.BorderWidth = 5;
                MinLine.IntervalOffset = MinVal;
                Chart1.ChartAreas["ChartArea1"].AxisY.StripLines.Add(MinLine);

                Chart1.ChartAreas["ChartArea1"].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None;
                Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 11, System.Drawing.FontStyle.Bold);
                Chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 11, System.Drawing.FontStyle.Bold);

                //string query = "SELECT dbo.Teams.Name AS Team, dbo.TeamTypes.Description AS Area, SUM(dbo.RptTeamWIP.WIPHours) AS WIPHours " +
                //                "FROM dbo.RptTeamWIP INNER JOIN " +
                //                                  "dbo.Teams ON dbo.RptTeamWIP.SewingTeamID = dbo.Teams.Id INNER JOIN " +
                //                                  "dbo.Teams AS Teams_1 ON dbo.RptTeamWIP.TeamID = Teams_1.Id INNER JOIN " +
                //                                  "dbo.TeamTypes ON Teams_1.TeamTypeId = dbo.TeamTypes.Id " +
                //                "WHERE(dbo.Teams.TeamTypeId = 1) AND(dbo.Teams.IsDeleted = 0) AND(dbo.Teams.IsActive = 1) AND (dbo.Teams.Name NOT LIKE '%JUMP%') " +
                //                "GROUP BY dbo.Teams.Name, dbo.TeamTypes.Description " +
                //                "ORDER BY Team, Area DESC";
                string query = "SELECT Team, Area, WIPHours FROM CMERptTeamWIP";
                DataTable dt = GetData(query);
                List<string> Areas = (from p in dt.AsEnumerable()
                                      select p.Field<string>("Area")).Distinct().ToList();




                foreach (var Area in Areas)
                {
                    string[] x = (from p in dt.AsEnumerable()
                                  where p.Field<string>("Area") == Area
                                  orderby p.Field<string>("Team") ascending
                                  select p.Field<string>("Team")).ToArray();

                    //Get the Total of Orders for each Area.
                    decimal[] y = (from p in dt.AsEnumerable()
                                   where p.Field<string>("Area") == Area
                                   orderby p.Field<string>("Team") ascending
                                   select p.Field<decimal>("WIPHours")).ToArray();
                    Chart1.Series.Add(new Series(Area));
                    Chart1.Series[Area].IsValueShownAsLabel = true;
                    Chart1.Series[Area].LabelForeColor = System.Drawing.Color.White;
                    Chart1.Series[Area].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
                    Chart1.Series[Area].ChartType = SeriesChartType.StackedColumn;
                    Chart1.Series[Area].Points.DataBindXY(x, y);
                    Chart1.Width = 1750;
                    Chart1.Height = 700;
                    Chart1.ImageStorageMode = ImageStorageMode.UseImageLocation; //Added to show chart image in MVC
                    Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                    Chart1.ChartAreas["ChartArea1"].AxisY.Interval = YAxisInterval;
                }

                

            }
        }

        private static DataTable GetData(string query)
        {
            string constr = ConfigurationManager.ConnectionStrings["NerveCentreEntities"].ConnectionString;

            if ((constr.ToLower().StartsWith("metadata=")))
            {
                EntityConnectionStringBuilder RefineConStr = new EntityConnectionStringBuilder(constr);
                constr = RefineConStr.ProviderConnectionString;

            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }
    }
}