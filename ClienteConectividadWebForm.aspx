<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClienteConectividadWebForm.aspx.cs" Async="true" Inherits="UMTransporte.ClienteConectividadWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<?xml version="1.0" encoding="utf-8"?>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table runat="server" GridLines="Both" Width="100%">
                <asp:TableRow runat="server" Height="200px">
                    <asp:TableCell VerticalAlign="Top" runat="server">
                        <asp:Label Font-Bold="true" Text="DATOS DEL PAQUETE:" runat="server"></asp:Label><br /><br />
                        <asp:Literal Text="Largo:" runat="server"></asp:Literal>
                        <asp:TextBox Width="100" runat="server"></asp:TextBox>
                        <asp:Literal Text="centimetros" runat="server"></asp:Literal><br />
                        <asp:Literal Text="Ancho:" runat="server"></asp:Literal>
                        <asp:TextBox Width="100" runat="server"></asp:TextBox>
                        <asp:Literal Text="centimetros" runat="server"></asp:Literal><br />
                        <asp:Literal Text="Alto:" runat="server"></asp:Literal>
                        <asp:TextBox Width="100" runat="server"></asp:TextBox>
                        <asp:Literal Text="centimetros" runat="server"></asp:Literal><br /><br />
                        <asp:Literal Text="Peso:" runat="server"></asp:Literal>
                        <asp:TextBox Width="50" runat="server"></asp:TextBox>
                        <asp:Literal Text="Kg" runat="server"></asp:Literal><br />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div>
            <asp:Table runat="server" GridLines="Both" Height="200px" Width="100%">
                <asp:TableRow runat="server" Height="200px">
                    <asp:TableCell runat="server" Width="400" BorderColor="Gray" BorderWidth="1">
                        <asp:Label Font-Bold="true" runat="server" Text="ORIGEN"></asp:Label><br /><br />
                         <asp:Table runat="server">
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell Width="230">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Calle:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="PCalleTextBox" Width="300" runat="server"/>
                                </asp:TableCell>
                                <asp:TableCell Width="300">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Número:"/>
                                        <asp:TextBox ID="PNumeroTextBox" Width="60" runat="server"/>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell>
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Block, Pasaje, Villa o Recinto:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="PBPVRTextBox" Text="" Width="300" runat="server"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label Font-Names="Calibri" runat="server" Font-Size="Smaller" Text="(Nombre y número si corresponde)"/>
                                </asp:TableCell>
                            </asp:TableRow>
                         </asp:Table>
                         <asp:Table runat="server">
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell Width="140">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Region:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="xRegionOrigen" Width="230" runat="server"/>
                                </asp:TableCell >
                                <asp:TableCell Width="300">
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell Width="140">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Comuna:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="xComunaOrigen" Width="230" runat="server"/>
                                </asp:TableCell>
                                <asp:TableCell Width="300">
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell Width="140">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Ciudad:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="xCiudadOrigen" Width="230" runat="server"/>
                                </asp:TableCell >
                                <asp:TableCell Width="300">
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell Width="140">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Código Postal:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="CPostalTextBox" Width="160" runat="server"/>
                                </asp:TableCell >
                                <asp:TableCell Width="300">
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="50"  BorderColor="Gray" BorderWidth="1">
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="400" BorderColor="Gray" BorderWidth="1">
                        <asp:Label Font-Bold="true" runat="server" Text="DESTINO"></asp:Label><br /><br />
                         <asp:Table runat="server">
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell Width="230">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Calle:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="TextBox1" Width="300" runat="server"/>
                                </asp:TableCell>
                                <asp:TableCell Width="300">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Número:"/>
                                        <asp:TextBox ID="TextBox2" Width="60" runat="server"/>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell>
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Block, Pasaje, Villa o Recinto:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="TextBox3" Text="" Width="300" runat="server"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label Font-Names="Calibri" runat="server" Font-Size="Smaller" Text="(Nombre y número si corresponde)"/>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                         <asp:Table runat="server">
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell Width="140">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Region:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="xRegionDestino" Width="230" runat="server"/>
                                </asp:TableCell>
                                <asp:TableCell Width="300">
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell Width="140">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Ciudad:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="xCiudadDestino" Width="230" runat="server"/>
                                </asp:TableCell>
                                <asp:TableCell Width="300">
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell Width="140">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Comuna:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="xComunaDestino" Width="230" runat="server"/>
                                </asp:TableCell >
                                <asp:TableCell Width="300">
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell Width="20"/>
                                <asp:TableCell Width="140">
                                    <asp:Label Font-Names="Calibri" runat="server" Text="Código Postal:"/>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="TextBox4" Width="160" runat="server"/>
                                </asp:TableCell >
                                <asp:TableCell Width="300">
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>

        <div>
            <asp:Table runat="server" Width="100%">
                <asp:TableRow runat="server">
                    <asp:TableCell Text="ENVIO POR:" Width="200" HorizontalAlign="Center" runat="server">
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:RadioButton GroupName="TransporteRadioButton" ID="EmpresaTransporteChilexpressRadioButton" Text="Chilexpress" OnCheckedChanged="EmpresaTransporteRadioButton_SelectedIndexChanged" Font-Bold="true" CellSpacing="40" RepeatDirection="Horizontal" runat="server" AutoPostBack="True" />  
                        <asp:RadioButton GroupName="TransporteRadioButton" ID="EmpresaTransporteCorreosRadioButton" Text="Correos de Chile" OnCheckedChanged="EmpresaTransporteRadioButton_SelectedIndexChanged" Font-Bold="true" CellSpacing="40" RepeatDirection="Horizontal" runat="server" AutoPostBack="True" />  
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>

        <div>
             <asp:Table runat="server" Height="50" Width="100%">
                <asp:TableRow runat="server">
                    <asp:TableCell Width="200" HorizontalAlign="Left" runat="server">
                        <asp:Label Font-Bold="true" Text="VALOR DEL TRANSPORTE:  $ " runat="server"></asp:Label>
                            <asp:TextBox runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
       </div>
        <div>
             <asp:Table runat="server" Height="50" Width="100%">
                <asp:TableRow runat="server">
                    <asp:TableCell HorizontalAlign="Left" runat="server">
                        <br />
                        <asp:Label runat="server" Text="REPORTE DE LA OPERACION: "></asp:Label> 
                        <asp:Label ID="ReporteOperacionTextBox" Text="CORRECTO O FALLA" Font-Bold="true" ForeColor="Green" Height="300" Width="600" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
       </div>
    </form>
</body>
</html>
