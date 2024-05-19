<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="FamilyList.aspx.cs" Inherits="Shahab.CensusRreport.Components.FamilyList" %>
<asp:Content ID="cphMiddle" ContentPlaceHolderID="cphMiddle" runat="server">
    <asp:GridView ID="gvFamilyList" runat="server" CssClass="grid" GridLines="None" BorderWidth="0" AllowPaging="true" AutoGenerateColumns="false" OnRowDataBound="gvFamilyList_RowDataBound">
        <HeaderStyle CssClass="headerTable" HorizontalAlign="Right" />
        <AlternatingRowStyle CssClass="alternateTable" />
        <RowStyle CssClass="itemTable" />
        <Columns>
            <asp:TemplateField ItemStyle-Width="20">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FamilyId" Visible="false"></asp:BoundField>
            <asp:TemplateField>
                <ItemStyle Width="20" HorizontalAlign="Center" />
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FamilyNumber" ItemStyle-Width="80"></asp:BoundField>
            <asp:TemplateField ItemStyle-Width="120">
                <ItemTemplate>
                    <%# Eval("Members") == null ? "---" : Eval("Members[0].FirstName") + " " +  Eval("Members[0].LastName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="90">
                <ItemTemplate>
                    <%#Eval("PlaceInfo.ParentInfo.Name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100">
                <ItemTemplate>
                    <%#Eval("PlaceInfo.Name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PostalCode" ItemStyle-Width="80"></asp:BoundField>
            <asp:TemplateField ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate> 
                    <%# (Eval("Members") == null || Eval("Members[0].NationalCode") == "") ? "---" : Eval("Members[0].NationalCode")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="140">
                <ItemTemplate>
                    <%#Eval("FamilyTypeInfo.Value")%>
                    <asp:Label ID="lblFamilyType" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
