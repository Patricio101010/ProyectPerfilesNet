using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SistemaPerfilesNET.AL.DataLog;
using SistemaPerfilesNET.AL.Entidades;
using SistemaPerfilesNET.BLL.Componentes;
using SistemaPerfilesNET.BLL.Interfaz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SistemaPerfilesNET.AL.Entidades.ModeloTableEN;

namespace SistemaPerfilesNET.UI.Modulo.AdministracionPerfiles.MantenedorBitacora
{
    public partial class Bitacora : System.Web.UI.Page
    {
        [WebMethod]
        public static DataTableResponse<BitacoraEN> GetListaBitacora(string ClientParameters)
        {
            string Method = "GetListaBitacora";
            try
            {
                IBitacoraBLL ObjBitacora = new BitacoraBLL();
                DataTableParameterBitacora dtp = JsonConvert.DeserializeObject<DataTableParameterBitacora>(ClientParameters);
                List<BitacoraEN> GetList = ObjBitacora.GetListaBitacora(dtp.start, dtp.length, dtp.search.value, out int total, dtp.order[0].column, dtp.order[0].dir, dtp.Sistema, dtp.FechaMin, dtp.FechaMax, dtp.Perfil, dtp.UsuarioIN, dtp.NroPuerta);

                string a = dtp.search.value;

                return new DataTableResponse<BitacoraEN>() { draw = dtp.draw, recordsFiltered = total, recordsTotal = total, data = GetList };
            }
            catch (Exception ex)
            {
                NLog.ArchivoLogStactic("Bitacora", Method, ex);
                return new DataTableResponse<BitacoraEN> { draw = 0, recordsFiltered = 0, recordsTotal = 0, data = null };
            }
        }

        ClsMensaje ClsMensaje = new ClsMensaje();
        readonly IDropDownListBLL ObjDropDown = new DropDownListBLL();
        readonly IBitacoraBLL ObjBitacora = new BitacoraBLL();
        private readonly NLog ObjLog = new NLog();
        string Usuario, Method;
        readonly string Interfaz = "Bitacora", MensajeError = "Se ha producido un error en el sistema. Por favor, comunícate con el soporte técnico si el problema persiste. Lamentamos las molestias.";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Method = "Page_Load";

                // Usuario **********
                if (Session["usr"] == null)
                {
                    Response.Redirect("~/SingIn.aspx", false);
                    return;
                }
                else
                {
                    Usuario = Session["usr"].ToString();
                }

                if (!IsPostBack)
                {
                    Label lblMensaje = (Label)this.Page.FindControl("ctl00$lblTitulo");
                    lblMensaje.Text = "Consulta de bitácora";
                    Session["Interfaz"] = "11000";

                    CargaDropDrown(dpSistema, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.Sistema));
                    CargaDropDrown(dpPerfiles, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.Perfil));
                    CargaDropDrown(dpUsuario, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.Usuario));

                    txtFechaDesde.Text = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd");
                    txtFechaHasta.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        #region "CARGA DRONW"

        protected void dpSistema_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Method = "dpSistema_SelectedIndexChanged";
                CargaDropDrown(dpPuertas, ObjDropDown.GetCargaDropList(DropDownListEN.OpcionDropdown.Puerta, int.Parse(dpSistema.SelectedValue)));
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        private void CargaDropDrown(DropDownList dp, List<DropDownListEN> GetLista)
        {
            dp.Items.Clear();
            dp.DataSource = GetLista;
            dp.DataValueField = "Codigo";
            dp.DataTextField = "Descripcion";
            dp.DataBind();
            dp.ClearSelection();
            dp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("SELECCIONAR", "0"));

            dp.SelectedValue = "0";
        }

        #endregion

        #region "EXPORTAR PDF"

        protected void BtnExportaPDF_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnExportaPDF_ServerClick";
                string CodigoSistema = "0", FechaDesde = "19000101", FechaHasta = "99991231", Perfil = "0", Usuario = "0", CodigoPuerta = "0";

                if (txtFechaDesde.Text != "")
                {
                    FechaDesde = DateTime.Parse(txtFechaDesde.Text).ToString("yyyyMMdd");
                }

                if (txtFechaHasta.Text != "")
                {
                    FechaHasta = DateTime.Parse(txtFechaHasta.Text).ToString("yyyyMMdd");
                }

                if (dpSistema.SelectedValue != "0")
                {
                    CodigoSistema = dpSistema.SelectedValue;
                }

                if (dpPerfiles.SelectedValue != "0")
                {
                    Perfil = dpPerfiles.SelectedValue;
                }

                if (dpUsuario.SelectedValue != "0")
                {
                    Usuario = dpUsuario.SelectedItem.ToString();
                }

                if (dpPuertas.SelectedValue != "0")
                {
                    CodigoPuerta = dpPuertas.SelectedItem.ToString();
                }

                List<BitacoraEN> GetList = ObjBitacora.GetListaBitacoraExportar(CodigoSistema, FechaDesde, FechaHasta, Perfil, Usuario, CodigoPuerta);
                generaPDF(GetList);
            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        public void generaPDF(List<BitacoraEN> GetList)
        {
            int numberColumns = 12;

            PdfPTable titulos = new PdfPTable(numberColumns - 2);
            titulos.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            titulos.WidthPercentage = 95;

            PdfPTable encabezados = new PdfPTable(numberColumns);
            encabezados.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            encabezados.WidthPercentage = 95;

            PdfPTable cuerpo = new PdfPTable(numberColumns);
            cuerpo.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            cuerpo.WidthPercentage = 95;

            PdfPTable footer = new PdfPTable(numberColumns);
            footer.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            footer.WidthPercentage = 95;

            PdfPTable datos = new PdfPTable(numberColumns);
            datos.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            datos.WidthPercentage = 95;

            //FONT
            var titleFontSubrayar = FontFactory.GetFont(FontFactory.COURIER_BOLD, 14, Font.UNDERLINE);
            var boldFont = FontFactory.GetFont(FontFactory.COURIER_BOLD, 6);
            var normalFont = FontFactory.GetFont(FontFactory.COURIER, 6);
            var datosFont = FontFactory.GetFont(FontFactory.COURIER, 6);

            //ESPACIO EN BLANCO
            PdfPCell linea2 = new PdfPCell(new Phrase(" "));
            linea2.Colspan = numberColumns;
            linea2.Border = 0;
            linea2.SpaceCharRatio = 800;

            PdfPCell linea3 = new PdfPCell(new Phrase(" "));
            linea3.Colspan = numberColumns;
            linea3.Border = 0;
            linea3.SpaceCharRatio = 1000;

            PdfPCell titleCell = new PdfPCell(new Phrase("BITÁCORA", titleFontSubrayar));
            titleCell.Colspan = 10;  //4
            titleCell.Border = 0;
            titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            titleCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            titulos.AddCell(titleCell);

            cuerpo.AddCell(linea3);

            //PREPARANDO EL CUERPO DE LA INFORMACION
            cuerpo.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            cuerpo.DefaultCell.Colspan = 2;
            cuerpo.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            cuerpo.AddCell(new Phrase("#", boldFont));
            cuerpo.AddCell(new Phrase("USUARIO", boldFont));
            cuerpo.AddCell(new Phrase("PERFIL", boldFont));
            cuerpo.AddCell(new Phrase("PUERTA", boldFont));
            cuerpo.AddCell(new Phrase("OBSERVACION", boldFont));
            cuerpo.AddCell(new Phrase("FECHA INGRESO", boldFont));

            using (MemoryStream m = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(doc, m);

                doc.Open();
                doc.AddAuthor("Dimension S.A.");
                doc.AddCreationDate();

                foreach (BitacoraEN item in GetList)
                {
                    datos.DefaultCell.Border = PdfPCell.NO_BORDER;
                    datos.DefaultCell.Colspan = 2;
                    datos.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    datos.AddCell(new Phrase(item.NroRegistro.ToString(), datosFont));
                    datos.AddCell(new Phrase(item.LoginUsuario, datosFont));
                    datos.AddCell(new Phrase(item.Perfil, datosFont));
                    datos.AddCell(new Phrase(item.PuertaAcceso, datosFont));
                    datos.AddCell(new Phrase(item.Observacion, datosFont));
                    datos.AddCell(new Phrase(item.FechaEvento.ToShortDateString(), datosFont));
                }

                doc.Add(titulos);
                doc.Add(encabezados);
                doc.Add(cuerpo);
                doc.Add(datos);

                doc.Close();
                writer.Close();

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment;filename=Bitacora.pdf");
                Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Dispose();

                Response.OutputStream.Close();
            }
        }

        #endregion

        #region "EXPORTAR EXCEL"

        protected void BtnExportaExcel_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Method = "BtnExportaExcel_ServerClick";
                string CodigoSistema = "0", FechaDesde = "19000101", FechaHasta = "99991231", Perfil = "0", Usuario = "0", CodigoPuerta = "0";

                if (txtFechaDesde.Text != "")
                {
                    FechaDesde = DateTime.Parse(txtFechaDesde.Text).ToString("yyyyMMdd");
                }

                if (txtFechaHasta.Text != "")
                {
                    FechaHasta = DateTime.Parse(txtFechaHasta.Text).ToString("yyyyMMdd");
                }

                if (dpSistema.SelectedValue != "0")
                {
                    CodigoSistema = dpSistema.SelectedValue;
                }

                if (dpPerfiles.SelectedValue != "0")
                {
                    Perfil = dpPerfiles.SelectedValue;
                }

                if (dpUsuario.SelectedValue != "0")
                {
                    Usuario = dpUsuario.SelectedItem.ToString();
                }


                if (dpPuertas.SelectedValue != "0")
                {
                    CodigoPuerta = dpPuertas.SelectedItem.ToString();
                }

                List<BitacoraEN> GetList = ObjBitacora.GetListaBitacoraExportar(CodigoSistema, FechaDesde, FechaHasta, Perfil, Usuario, CodigoPuerta);
                GenerateExcel(GetList);

            }
            catch (Exception ex)
            {
                ClsMensaje.Mensaje(Page, Page.Title, MensajeError, ClsMensaje.TipoMensaje._Error);
                ObjLog.ArchivoLog(Interfaz, Method, ex);
            }
        }

        public void GenerateExcel(List<BitacoraEN> GetList)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("BITÁCORA");
                int i = 3;

                ws.Cells[i, 1].Value = "BITÁCORA";
                using (ExcelRange rng = ws.Cells[i, 1, i, 6])
                {
                    rng.Merge = true;
                    rng.Style.Font.Bold = true;
                    rng.Style.Font.Size = 12;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                i += 2;

                ws.Cells[i, 1].Value = "#";
                ws.Cells[i, 2].Value = "USUARIO";
                ws.Cells[i, 3].Value = "PERFIL";
                ws.Cells[i, 4].Value = "PUERTA";
                ws.Cells[i, 5].Value = "OBSERVACIÓN";
                ws.Cells[i, 6].Value = "FECHA INGRESO";

                ws.Cells[i, 1, i, 6].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[i, 1, i, 6].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkGray);

                i++;
                foreach (BitacoraEN item in GetList)
                {
                    ws.Cells[i, 1].Value = item.NroRegistro.ToString();
                    ws.Cells[i, 2].Value = item.LoginUsuario;
                    ws.Cells[i, 3].Value = item.Perfil;
                    ws.Cells[i, 4].Value = item.PuertaAcceso;
                    ws.Cells[i, 5].Value = item.Observacion;
                    ws.Cells[i, 6].Value = item.FechaEvento.ToShortDateString();
                    i++;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Bitacora.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    pck.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);

                    Response.Flush();
                    Response.End();
                }
            }
        }

        #endregion
    }
}
