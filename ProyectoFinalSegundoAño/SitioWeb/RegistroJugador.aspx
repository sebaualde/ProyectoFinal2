<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageGeneral.master" AutoEventWireup="true" CodeFile="RegistroJugador.aspx.cs" Inherits="RegistroJugador" %>

<asp:Content ID="Content" ContentPlaceHolderID="cphContenido" Runat="Server">
    <p>
        <br />
        <table align="center" style="width: 518px;">
            <tr>
                <td style="width: 203px; text-align: center;">
                    CI:</td>
                <td style="width: 252px; text-align: center;">
                    <asp:TextBox ID="txtCI" runat="server" Width="250px"></asp:TextBox>
                </td>
                <td style="text-align: center; width: 77px">
                    <asp:Button ID="btnBuscar" runat="server" onclick="btnBuscar_Click" 
                        Text="Buscar" />
                </td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    Nombre Completo:</td>
                <td style="width: 252px; text-align: center;">
                    <asp:TextBox ID="txtNomCompleto" runat="server" Width="250px"></asp:TextBox>
                </td>
                <td style="text-align: center; width: 77px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center; height: 26px;">
                    Nombre Logueo:</td>
                <td style="width: 252px; text-align: center; height: 26px;">
                    <asp:TextBox ID="txtNomLogueo" runat="server" Width="250px"></asp:TextBox>
                </td>
                    </td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    Contraseña:</td>
                <td style="width: 252px; text-align: center;">
                    <asp:TextBox ID="txtContrasenia" runat="server" Width="250px" 
                        TextMode="Password"></asp:TextBox>
                </td>
                <td style="text-align: center; width: 77px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    Numero de cuenta bancaria:</td>
                <td style="width: 252px; text-align: center;">
                    <asp:TextBox ID="txtCuentaBancaria" runat="server" Width="250px"></asp:TextBox>
                </td>
                <td style="text-align: center; width: 77px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    Banco:</td>
                <td style="width: 252px; text-align: center;">
                    <asp:DropDownList ID="ddlBancos" runat="server" Height="25px" Width="250px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: center; width: 77px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    &nbsp;</td>
                <td style="width: 252px; text-align: center;">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: center; width: 77px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    &nbsp;</td>
                <td style="width: 252px; text-align: center;">
                    <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                        Text="Aceptar" />
&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" onclick="btnCancelar_Click" 
                        Text="Cancelar" />
                </td>
                <td style="text-align: center; width: 77px">
                    &nbsp;</td>
            </tr>
        </table>
    </p>
    <br />
    <br />
</asp:Content>

