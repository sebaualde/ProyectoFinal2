<%@ page title="" language="C#" masterpagefile="~/MasterPageGeneral.master" autoeventwireup="true" inherits="GenerarJugada, App_Web_34wi33zw" %>

<asp:Content ID="Content" ContentPlaceHolderID="cphContenido" Runat="Server">
    <br />
    <center>
        <asp:Panel ID="PanelSorteos" runat="server">
        <h2>
        Sorteos disponibles:</h2>
        <asp:Repeater ID="Repeater1" runat="server" 
            onitemcommand="Repeater1_ItemCommand">
        <ItemTemplate><div class="tablaListados">
                    <table>
                        <tr>
                            <td align="right">Fecha y Hora:</td>
                               
                            <td>
                                <asp:Label ID= "lblFechaHora" runat="server" Text='<%# Bind("FechaHora") %>'> </asp:Label>
                                <br />
                            </td>
                            
                            <td>
                                <asp:Button ID = "btnRealizarJugada" runat= "server" CommandName = "RealizarJugada" 
               Text= "Realizar Jugada" BackColor="#246898" ForeColor="White" BorderColor="#246898" BorderStyle="Double" />
                            </td>
                        </tr>
                    </table>
                    </div>
                </ItemTemplate>
        </asp:Repeater>
    
        </asp:Panel>
            <asp:Panel ID="PanelJugada" runat="server" Visible="False">
                <h2>
                    Realizar jugada para el sorteo:
                    <asp:Label ID="lblSorteoSeleccionado" runat="server" ForeColor="#006699" 
                        style="font-weight: 700"></asp:Label>
                </h2>
                <p>
                    Ingrese los números de su jugada:</p>
                <p>
                    <asp:TextBox ID="txtNumero1" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;-
                    <asp:TextBox ID="txtNumero2" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;-
                    <asp:TextBox ID="txtNumero3" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;-
                    <asp:TextBox ID="txtNumero4" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;-
                    <asp:TextBox ID="txtNumero5" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;-
                    <asp:TextBox ID="txtNumero6" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;-
                    <asp:TextBox ID="txtNumero7" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;-
                    <asp:TextBox ID="txtNumero8" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;-
                    <asp:TextBox ID="txtNumero9" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;-
                    <asp:TextBox ID="txtNumero10" runat="server" Width="30px"></asp:TextBox>
                    &nbsp;</p>
                <p>
                    <asp:Button ID="btnEnviarJugada" runat="server" onclick="btnEnviarJugada_Click" 
                        Text="Enviar Jugada" BackColor="#246898" ForeColor="White" BorderColor="#246898" BorderStyle="Double" />
                    &nbsp;<asp:Button ID="btnCancelar" runat="server" onclick="btnCancelar_Click" 
                        Text="Cancelar" BackColor="#246898" ForeColor="White" BorderColor="#246898" BorderStyle="Double"/>
                </p>
        </asp:Panel>
            <br />
        <asp:Label ID="lblMensaje" runat="server" EnableViewState="False"></asp:Label>
            </center>
    <br />
</asp:Content>

