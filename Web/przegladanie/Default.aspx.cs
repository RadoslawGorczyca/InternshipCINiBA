using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Text.RegularExpressions;


public partial class przegladanie_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            setEmptyDropDownListArea();
            setEmptyDropDownListStand();
        }
    }


    private void setEmptyDropDownListArea()
    {
        ListItem li;
        
        dropDownListArea.Items.Clear();

        li = new ListItem("Proszę najpierw wybrać piętro");
        dropDownListArea.Items.Add(li);
    }


    private void setEmptyDropDownListStand()
    {
        ListItem li;
        
        dropDownListStand.Items.Clear();

        li = new ListItem("Proszę najpierw wybrać strefę");
        dropDownListStand.Items.Add(li);
    }


    private void setEmptyGridViewStandFailure()
    {
        gridViewStandFailure.DataSourceID = null;
        gridViewStandFailure.DataBind();
    }


    protected void dropDownListFloor_DataBound(object sender, EventArgs e)
    {
        ListItem li;

        li = new ListItem("Proszę wybrać piętro");
        dropDownListFloor.Items.Insert(0, li);

        li = new ListItem("--------------------");
        dropDownListFloor.Items.Insert(1, li);
    }
    

    protected void dropDownListFloor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropDownListFloor.SelectedIndex > 1)
        {
            bindDropDownListArea();
            setEmptyDropDownListStand();
            setEmptyGridViewStandFailure();
        }
        else
        {
            if (dropDownListArea.Items.Count > 2)
            {
                setEmptyDropDownListArea();
                setEmptyDropDownListStand();
                setEmptyGridViewStandFailure();
            }
        }
    }


    private void bindDropDownListArea()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT area FROM areas WHERE floor = '");
        sb.Append(dropDownListFloor.SelectedValue);
        sb.Append("' ORDER BY area");

        accessDataSourceArea.SelectCommand = sb.ToString();

        dropDownListArea.Items.Clear();
        dropDownListArea.DataSourceID = "accessDataSourceArea";
        dropDownListArea.DataBind();
    }


    protected void dropDownListArea_DataBound(object sender, EventArgs e)
    {
        ListItem li;

        li = new ListItem("Proszę wybrać strefę");
        dropDownListArea.Items.Insert(0, li);

        li = new ListItem("--------------------");
        dropDownListArea.Items.Insert(1, li);
    }


    protected void dropDownListArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropDownListArea.SelectedIndex > 1)
        {
            bindDropDownListStand();
            setEmptyGridViewStandFailure();
        }
        else
        {
            if (dropDownListStand.Items.Count > 2)
            {
                setEmptyDropDownListStand();
                setEmptyGridViewStandFailure();
            }
        }
    }


    private void bindDropDownListStand()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT stand FROM areas WHERE floor = '");
        sb.Append(dropDownListFloor.SelectedValue);
        sb.Append("' AND area = '");
        sb.Append(dropDownListArea.SelectedValue);
        sb.Append("' ORDER BY stand");

        accessDataSourceStand.SelectCommand = sb.ToString();

        dropDownListStand.Items.Clear();
        dropDownListStand.DataSourceID = "accessDataSourceStand";
        dropDownListStand.DataBind();
    }



    protected void dropDownListStand_DataBound(object sender, EventArgs e)
    {
        ListItem li;

        li = new ListItem("Proszę wybrać stanowisko");
        dropDownListStand.Items.Insert(0, li);

        li = new ListItem("--------------------");
        dropDownListStand.Items.Insert(1, li);
    }


    protected void dropDownListStand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropDownListStand.SelectedIndex > 1)
            bindGridViewStandFailure();
        else
            setEmptyGridViewStandFailure();
    }


    private void bindGridViewStandFailure()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT sendDate, content, surname, comments, archived FROM failure WHERE floor = '");
        sb.Append(dropDownListFloor.SelectedValue);
        sb.Append("' AND area = '");
        sb.Append(dropDownListArea.SelectedValue);
        sb.Append("' AND stand = '");
        sb.Append(dropDownListStand.SelectedValue);
        sb.Append("' ORDER BY sendDate DESC");

        accessDataSourceStandFailure.SelectCommand = sb.ToString();

        gridViewStandFailure.DataSourceID = "accessDataSourceStandFailure";
        gridViewStandFailure.DataBind();
    }


    public static string getArchivedString(bool b)
    {
        return b ? "tak" : "nie";
    }


    public static string replaceCRLFWithBR(string s)
    {
        return Regex.Replace(s, "[\n\r]+", "<br />");
    }



    protected void gridViewStandFailure_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindGridViewStandFailure();
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (dropDownListStand.SelectedIndex < 2 || gridViewStandFailure.Rows.Count > 0)
            placeHolderEmptyGridViewStandFailure.Visible = false;
        else
            placeHolderEmptyGridViewStandFailure.Visible = true;
    }
}