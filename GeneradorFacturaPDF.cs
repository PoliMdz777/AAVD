using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Pantallas_alto_volumen_de_datos.Entidades;
using static Pantallas_alto_volumen_de_datos.DAO.HotelesDAO;

public static class GeneradorFacturaPDF
{


    // Método para generar PDF incluyendo datos del hotel y reservación
    //public static string GenerarFacturaPDF(
    //string folioFiscal,
    //string folioInterno,
    //string nombreCliente,
    //string rfcCliente,
    //DateTime fecha,
    //string domicilioFiscal,
    //string usoCFDI,
    //string formaPago,
    //decimal subtotal,
    //decimal iva,
    //decimal total,
    //List<(string descripcion, decimal precio)> servicios,
    //List<(string habitacion, decimal precio, int noches, decimal total)> habitaciones,
    //InfoHotelFactura datosHotel)
    //{
    //    string carpeta = @"C:\FacturasPDF";
    //    Directory.CreateDirectory(carpeta);

    //    string rutaArchivo = Path.Combine(carpeta, $"Factura_{folioInterno}.pdf");

    //    Document doc = new Document(PageSize.LETTER, 50, 50, 50, 50);
    //    PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
    //    doc.Open();

    //    var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
    //    var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);

    //    // Encabezado
    //    doc.Add(new Paragraph("ZAFIRO H&S", fuenteNegrita));
    //    doc.Add(new Paragraph($"Hotel: {datosHotel.Nombre} ({datosHotel.Ciudad}, {datosHotel.Estado}, {datosHotel.Pais})", fuenteNormal));
    //    doc.Add(new Paragraph($"Folio Interno: {folioInterno}", fuenteNormal));
    //    doc.Add(new Paragraph($"Folio Fiscal: {folioFiscal}", fuenteNormal));
    //    doc.Add(new Paragraph($"Fecha: {fecha:dd/MM/yyyy}", fuenteNormal));
    //    doc.Add(new Paragraph($"Cliente: {nombreCliente}", fuenteNormal));
    //    doc.Add(new Paragraph($"RFC: {rfcCliente}", fuenteNormal));
    //    doc.Add(new Paragraph($"Domicilio Fiscal: {domicilioFiscal}", fuenteNormal));
    //    doc.Add(new Paragraph($"Uso CFDI: {usoCFDI}", fuenteNormal));
    //    doc.Add(new Paragraph($"Forma de Pago: {formaPago}", fuenteNormal));
    //    doc.Add(new Paragraph("\n"));

    //    // Tabla de habitaciones
    //    doc.Add(new Paragraph("Habitaciones Reservadas:", fuenteNegrita));
    //    PdfPTable tablaHabitaciones = new PdfPTable(4);
    //    tablaHabitaciones.WidthPercentage = 100;
    //    tablaHabitaciones.SetWidths(new float[] { 2f, 2f, 2f, 2f });

    //    tablaHabitaciones.AddCell("Habitación");
    //    tablaHabitaciones.AddCell("Precio por noche");
    //    tablaHabitaciones.AddCell("Noches");
    //    tablaHabitaciones.AddCell("Total");

    //    foreach (var hab in habitaciones)
    //    {
    //        decimal totalHab = hab.precio * hab.noches;
    //        tablaHabitaciones.AddCell(hab.habitacion);
    //        tablaHabitaciones.AddCell(hab.precio.ToString("C"));
    //        tablaHabitaciones.AddCell(hab.noches.ToString());
    //        tablaHabitaciones.AddCell(totalHab.ToString("C"));
    //    }

    //    doc.Add(tablaHabitaciones);
    //    doc.Add(new Paragraph("\n"));

    //    // Tabla de servicios
    //    doc.Add(new Paragraph("Servicios Adicionales:", fuenteNegrita));
    //    PdfPTable tablaServicios = new PdfPTable(3);
    //    tablaServicios.WidthPercentage = 100;
    //    tablaServicios.SetWidths(new float[] { 1f, 4f, 2f });

    //    tablaServicios.AddCell("Cant.");
    //    tablaServicios.AddCell("Descripción");
    //    tablaServicios.AddCell("Precio");

    //    foreach (var servicio in servicios)
    //    {
    //        tablaServicios.AddCell("1");
    //        tablaServicios.AddCell(servicio.descripcion);
    //        tablaServicios.AddCell(servicio.precio.ToString("C"));
    //    }

    //    doc.Add(tablaServicios);
    //    doc.Add(new Paragraph("\n"));

    //    // Totales
    //    PdfPTable totales = new PdfPTable(2);
    //    totales.WidthPercentage = 40;
    //    totales.HorizontalAlignment = Element.ALIGN_RIGHT;

    //    totales.AddCell("Subtotal:");
    //    totales.AddCell(subtotal.ToString("C"));
    //    totales.AddCell("IVA:");
    //    totales.AddCell(iva.ToString("C"));
    //    totales.AddCell("TOTAL:");
    //    totales.AddCell(total.ToString("C"));

    //    doc.Add(totales);

    //    doc.Close();
    //    return rutaArchivo;
    //}
    public static string GenerarFacturaPDF(
    string folioFiscal,
    string folioInterno,
    string nombreCliente,
    string rfcCliente,
    DateTime fecha,
    string domicilioFiscal,
    string usoCFDI,
    string formaPago,
    decimal subtotal,
    decimal iva,
    decimal total,
    List<(string descripcion, decimal precio)> servicios,
    List<(string habitacion, decimal precio, int noches, decimal total)> habitaciones,
    InfoHotelFactura datosHotel)
    {
        string carpeta = @"C:\FacturasPDF";
        Directory.CreateDirectory(carpeta);

        string rutaArchivo = Path.Combine(carpeta, $"Factura_{folioInterno}.pdf");

        Document doc = new Document(PageSize.LETTER, 50, 50, 50, 50);
        PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
        doc.Open();

        var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);

        // Encabezado
        doc.Add(new Paragraph("ZAFIRO H&S", fuenteNegrita));
        doc.Add(new Paragraph($"Hotel: {datosHotel.Nombre}, {datosHotel.Ciudad}, {datosHotel.Estado}, {datosHotel.Pais}", fuenteNormal));
        doc.Add(new Paragraph($"Folio Interno: {folioInterno}", fuenteNormal));
        doc.Add(new Paragraph($"Folio Fiscal: {folioFiscal}", fuenteNormal));
        doc.Add(new Paragraph($"Fecha: {fecha.ToShortDateString()}", fuenteNormal));
        doc.Add(new Paragraph($"Cliente: {nombreCliente}", fuenteNormal));
        doc.Add(new Paragraph($"RFC: {rfcCliente}", fuenteNormal));
        doc.Add(new Paragraph($"Domicilio Fiscal: {domicilioFiscal}", fuenteNormal));
        doc.Add(new Paragraph($"Forma de Pago: {formaPago}", fuenteNormal));
        doc.Add(new Paragraph($"Uso CFDI: {usoCFDI}", fuenteNormal));
        doc.Add(new Paragraph("\n"));

        // Tabla de habitaciones
        PdfPTable tablaHab = new PdfPTable(5);
        tablaHab.WidthPercentage = 100;
        tablaHab.SetWidths(new float[] { 2, 2, 2, 2, 2 });

        tablaHab.AddCell("Habitación");
        tablaHab.AddCell("Precio/Noche");
        tablaHab.AddCell("Noches");
        tablaHab.AddCell("Subtotal");
        tablaHab.AddCell("Total con IVA (1.18)");

        decimal totalHabitaciones = 0;
        foreach (var h in habitaciones)
        {
            decimal sub = h.precio * h.noches;
            decimal totalIVA = sub * 1.18m;
            totalHabitaciones += totalIVA;

            tablaHab.AddCell(h.habitacion);
            tablaHab.AddCell(h.precio.ToString("C"));
            tablaHab.AddCell(h.noches.ToString());
            tablaHab.AddCell(sub.ToString("C"));
            tablaHab.AddCell(totalIVA.ToString("C"));
        }
        doc.Add(new Paragraph("Habitaciones Reservadas:", fuenteNegrita));
        doc.Add(tablaHab);
        doc.Add(new Paragraph("\n"));

        // Tabla de servicios
        PdfPTable tablaServicios = new PdfPTable(4);
        tablaServicios.WidthPercentage = 100;
        tablaServicios.SetWidths(new float[] { 1, 4, 2, 2 });

        tablaServicios.AddCell("Cant.");
        tablaServicios.AddCell("Descripción");
        tablaServicios.AddCell("Precio");
        tablaServicios.AddCell("Total");

        foreach (var s in servicios)
        {
            tablaServicios.AddCell("1");
            tablaServicios.AddCell(s.descripcion);
            tablaServicios.AddCell(s.precio.ToString("C"));
            tablaServicios.AddCell(s.precio.ToString("C"));
        }

        doc.Add(new Paragraph("Servicios Adicionales:", fuenteNegrita));
        doc.Add(tablaServicios);
        doc.Add(new Paragraph("\n"));

        // Totales finales
        doc.Add(new Paragraph($"Subtotal: {subtotal:C}", fuenteNormal));
        doc.Add(new Paragraph($"IVA (18%): {iva:C}", fuenteNormal));
        doc.Add(new Paragraph($"Total: {total:C}", fuenteNegrita));
        

        doc.Close();
        return rutaArchivo;
    }


    public static string GenerarFacturaXML(
    string folioFiscal,
    string folioInterno,
    string nombreCliente,
    string rfc,
    DateTime fecha,
    string domicilioFiscal,
    string usoCFDI,
    string formaPago,
    decimal subtotal,
    decimal iva,
    decimal total,
    List<(string descripcion, decimal precio)> servicios,
    List<(string habitacion, decimal precio, int noches, decimal total)> habitaciones,
    InfoHotelFactura datosHotel)
    {
        string carpeta = @"C:\FacturasXML";
        Directory.CreateDirectory(carpeta);

        string rutaArchivo = Path.Combine(carpeta, $"Factura_{folioInterno}.xml");

        using (StreamWriter writer = new StreamWriter(rutaArchivo))
        {
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            writer.WriteLine("<Factura>");

            writer.WriteLine($"  <FolioFiscal>{folioFiscal}</FolioFiscal>");
            writer.WriteLine($"  <FolioInterno>{folioInterno}</FolioInterno>");
            writer.WriteLine($"  <Fecha>{fecha:yyyy-MM-ddTHH:mm:ss}</Fecha>");

            // Cliente
            writer.WriteLine("  <Cliente>");
            writer.WriteLine($"    <Nombre>{nombreCliente}</Nombre>");
            writer.WriteLine($"    <RFC>{rfc}</RFC>");
            writer.WriteLine($"    <DomicilioFiscal>{domicilioFiscal}</DomicilioFiscal>");
            writer.WriteLine($"    <UsoCFDI>{usoCFDI}</UsoCFDI>");
            writer.WriteLine("  </Cliente>");

            // Hotel
            writer.WriteLine("  <Hotel>");
            writer.WriteLine($"    <Nombre>{datosHotel.Nombre}</Nombre>");
            writer.WriteLine($"    <Ciudad>{datosHotel.Ciudad}</Ciudad>");
            writer.WriteLine($"    <Estado>{datosHotel.Estado}</Estado>");
            writer.WriteLine($"    <Pais>{datosHotel.Pais}</Pais>");
            writer.WriteLine($"    <CodigoSAT>{datosHotel.CodigoSAT}</CodigoSAT>");
            writer.WriteLine($"    <UnidadSAT>{datosHotel.UnidadServicio}</UnidadSAT>");
            writer.WriteLine("  </Hotel>");

            // Habitaciones
            writer.WriteLine("  <Habitaciones>");
            foreach (var h in habitaciones)
            {
                decimal subtotalHab = h.precio * h.noches;
                decimal totalHabIVA = subtotalHab * 1.18m;

                writer.WriteLine("    <Habitacion>");
                writer.WriteLine($"      <Numero>{h.habitacion}</Numero>");
                writer.WriteLine($"      <PrecioPorNoche>{h.precio}</PrecioPorNoche>");
                writer.WriteLine($"      <Noches>{h.noches}</Noches>");
                writer.WriteLine($"      <Subtotal>{subtotalHab:0.00}</Subtotal>");
                writer.WriteLine($"      <TotalConIVA>{totalHabIVA:0.00}</TotalConIVA>");
                writer.WriteLine("    </Habitacion>");
            }
            writer.WriteLine("  </Habitaciones>");

            // Servicios
            writer.WriteLine("  <Servicios>");
            foreach (var s in servicios)
            {
                writer.WriteLine("    <Servicio>");
                writer.WriteLine($"      <Descripcion>{s.descripcion}</Descripcion>");
                writer.WriteLine($"      <Precio>{s.precio:0.00}</Precio>");
                writer.WriteLine("    </Servicio>");
            }
            writer.WriteLine("  </Servicios>");

            // Totales
            writer.WriteLine("  <Totales>");
            writer.WriteLine($"    <Subtotal>{subtotal:0.00}</Subtotal>");
            writer.WriteLine($"    <IVA>{iva:0.00}</IVA>");
            writer.WriteLine($"    <Total>{total:0.00}</Total>");
            writer.WriteLine("  </Totales>");

            // Forma de pago
            writer.WriteLine($"  <FormaPago>{formaPago}</FormaPago>");

            writer.WriteLine("</Factura>");
        }

        return rutaArchivo;
    }


    //public static string GenerarFacturaXML(
    // string folioFiscal,
    // string folioInterno,
    // string nombreCliente,
    // string rfc,
    // string domicilioFiscal,
    // string usoCFDI,
    // string formaPago,
    // DateTime fecha,
    // decimal subtotal,
    // decimal iva,
    // decimal total,
    // List<(string descripcion, decimal precio)> servicios,
    // List<(string habitacion, decimal precio, int noches, decimal total)> habitaciones,
    // InfoHotelFactura datosHotel)
    //{
    //    string carpeta = @"C:\FacturasXML";
    //    Directory.CreateDirectory(carpeta);

    //    string rutaArchivo = Path.Combine(carpeta, $"Factura_{folioInterno}.xml");

    //    using (StreamWriter writer = new StreamWriter(rutaArchivo))
    //    {
    //        writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
    //        writer.WriteLine("<Factura>");
    //        writer.WriteLine($"  <FolioFiscal>{folioFiscal}</FolioFiscal>");
    //        writer.WriteLine($"  <FolioInterno>{folioInterno}</FolioInterno>");
    //        writer.WriteLine($"  <Fecha>{fecha:yyyy-MM-ddTHH:mm:ss}</Fecha>");

    //        // Datos del cliente
    //        writer.WriteLine("  <Cliente>");
    //        writer.WriteLine($"    <Nombre>{nombreCliente}</Nombre>");
    //        writer.WriteLine($"    <RFC>{rfc}</RFC>");
    //        writer.WriteLine($"    <DomicilioFiscal>{domicilioFiscal}</DomicilioFiscal>");
    //        writer.WriteLine($"    <UsoCFDI>{usoCFDI}</UsoCFDI>");
    //        writer.WriteLine("  </Cliente>");

    //        // Datos del hotel
    //        writer.WriteLine("  <Hotel>");
    //        writer.WriteLine($"    <Nombre>{datosHotel.Nombre}</Nombre>");
    //        writer.WriteLine($"    <Ciudad>{datosHotel.Ciudad}</Ciudad>");
    //        writer.WriteLine($"    <Estado>{datosHotel.Estado}</Estado>");
    //        writer.WriteLine($"    <Pais>{datosHotel.Pais}</Pais>");
    //        writer.WriteLine($"    <CodigoSAT>{datosHotel.CodigoSAT}</CodigoSAT>");
    //        writer.WriteLine($"    <UnidadSAT>{datosHotel.UnidadServicio}</UnidadSAT>");
    //        writer.WriteLine("  </Hotel>");

    //        // Habitaciones
    //        writer.WriteLine("  <Habitaciones>");
    //        foreach (var h in habitaciones)
    //        {
    //            writer.WriteLine("    <Habitacion>");
    //            writer.WriteLine($"      <Numero>{h.habitacion}</Numero>");
    //            writer.WriteLine($"      <PrecioPorNoche>{h.precio}</PrecioPorNoche>");
    //            writer.WriteLine($"      <Noches>{h.noches}</Noches>");
    //            writer.WriteLine($"      <Total (1.18 IVA)>{(h.precio * h.noches * 1.18m):0.00}</Total>");
    //            writer.WriteLine("    </Habitacion>");
    //        }
    //        writer.WriteLine("  </Habitaciones>");

    //        // Servicios
    //        writer.WriteLine("  <Servicios>");
    //        foreach (var servicio in servicios)
    //        {
    //            writer.WriteLine("    <Servicio>");
    //            writer.WriteLine($"      <Descripcion>{servicio.descripcion}</Descripcion>");
    //            writer.WriteLine($"      <Precio>{servicio.precio}</Precio>");
    //            writer.WriteLine("    </Servicio>");
    //        }
    //        writer.WriteLine("  </Servicios>");

    //        // Totales
    //        writer.WriteLine("  <Totales>");
    //        writer.WriteLine($"    <Subtotal>{subtotal}</Subtotal>");
    //        writer.WriteLine($"    <IVA>{iva}</IVA>");
    //        writer.WriteLine($"    <Total>{total}</Total>");
    //        writer.WriteLine("  </Totales>");

    //        // Forma de pago
    //        writer.WriteLine($"  <FormaPago>{formaPago}</FormaPago>");

    //        writer.WriteLine("</Factura>");
    //    }

    //    return rutaArchivo;
    //}




}




