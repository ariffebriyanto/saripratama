Imports Stimulsoft.Report

Public Class WebFormRpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("q") IsNot Nothing Then
            Dim report As StiReport = New StiReport()
            report.Load(Server.MapPath("~/Reports/DO.mrt"))
            report.Compile()
            report("id") = Request.QueryString("q").ToString
            report.Render()
            StiWebViewer1.Report = report
        ElseIf Request.QueryString("f") IsNot Nothing Then
            Dim report As StiReport = New StiReport()
            report.Load(Server.MapPath("~/Reports/Faktur.mrt"))
            report.Compile()
            report("id") = Request.QueryString("f").ToString
            report.Render()
            StiWebViewer1.Report = report
        ElseIf Request.QueryString("type") IsNot Nothing Then
            If Request.QueryString("type") = "labarugi" Then
                If Request.QueryString("bln") IsNot Nothing AndAlso Request.QueryString("thn") IsNot Nothing AndAlso Request.QueryString("val") IsNot Nothing Then
                    Dim report As StiReport = New StiReport()
                    report.Load(Server.MapPath("~/Reports/repLabaRugi1.mrt"))
                    report.Compile()
                    report("bln") = Request.QueryString("bln").ToString()
                    report("thn") = Request.QueryString("thn").ToString()
                    report("val") = Request.QueryString("val").ToString()
                    report("bulan") = Request.QueryString("bln").ToString()
                    report("tahun") = Request.QueryString("thn").ToString()
                    report("valuta") = Request.QueryString("val").ToString()
                    report.Render()
                    StiWebViewer1.Report = report
                End If
            ElseIf Request.QueryString("type") = "aktivapasiva" Then
                If Request.QueryString("bln") IsNot Nothing AndAlso Request.QueryString("thn") IsNot Nothing AndAlso Request.QueryString("val") IsNot Nothing Then
                    Dim report As StiReport = New StiReport()
                    report.Load(Server.MapPath("~/Reports/repAktivaPasiva.mrt"))
                    report.Compile()
                    report("bln") = Request.QueryString("bln").ToString()
                    report("thn") = Request.QueryString("thn").ToString()
                    report("val") = Request.QueryString("val").ToString()
                    report("bulan") = Request.QueryString("bln").ToString()
                    report("tahun") = Request.QueryString("thn").ToString()
                    report("valuta") = Request.QueryString("val").ToString()
                    report.Render()
                    StiWebViewer1.Report = report
                End If
            ElseIf Request.QueryString("type") = "aktivapasivarekap" Then
                If Request.QueryString("bln") IsNot Nothing AndAlso Request.QueryString("thn") IsNot Nothing AndAlso Request.QueryString("val") IsNot Nothing Then
                    Dim report As StiReport = New StiReport()
                    report.Load(Server.MapPath("~/Reports/repAktivaPasivaRekap.mrt"))
                    report.Compile()
                    report("bln") = Request.QueryString("bln").ToString()
                    report("thn") = Request.QueryString("thn").ToString()
                    report("val") = Request.QueryString("val").ToString()
                    report("bulan") = Request.QueryString("bln").ToString()
                    report("tahun") = Request.QueryString("thn").ToString()
                    report("valuta") = Request.QueryString("val").ToString()
                    report.Render()
                    StiWebViewer1.Report = report
                End If
            ElseIf Request.QueryString("type") = "rencanakirim" Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim report As StiReport = New StiReport()
                    report.Load(Server.MapPath("~/Reports/rencana_krm_sales1New.mrt"))
                    report.Compile()
                    '  report("id") = "04/10/2018 9:48:44"
                    report("id") = Request.QueryString("id")

                    report.Render()
                    StiWebViewer1.Report = report
                End If
            ElseIf Request.QueryString("type") = "returpo" Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim report As StiReport = New StiReport()
                    report.Load(Server.MapPath("~/Reports/Retur_PO.mrt"))
                    report.Compile()
                    report("id") = Request.QueryString("id").ToString()
                    report.Render()
                    StiWebViewer1.Report = report
                End If
            ElseIf Request.QueryString("type") = "mutasicabang" Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim report As StiReport = New StiReport()
                    report.Load(Server.MapPath("~/Reports/Mutasi_keluar_cabang.mrt"))
                    report.Compile()
                    report("id") = Request.QueryString("id").ToString()
                    report.Render()
                    StiWebViewer1.Report = report
                End If
            ElseIf Request.QueryString("type") = "mutasimasukcabang" Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim report As StiReport = New StiReport()
                    report.Load(Server.MapPath("~/Reports/Mutasi_masuk_cabang.mrt"))
                    report.Compile()
                    report("id") = Request.QueryString("id").ToString()
                    report.Render()
                    StiWebViewer1.Report = report
                End If
            ElseIf Request.QueryString("type") = "suratjalan" Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim report As StiReport = New StiReport()
                    report.Load(Server.MapPath("~/Reports/SuratJalan.mrt"))
                    report.Compile()
                    report("id") = Request.QueryString("id").ToString()
                    report.Render()
                    StiWebViewer1.Report = report
                End If
            ElseIf Request.QueryString("type") = "invoicesementara" Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim report As StiReport = New StiReport()
                    report.Load(Server.MapPath("~/Reports/InvoiceSementara.mrt"))
                    report.Compile()
                    report("id") = Request.QueryString("id").ToString()
                    report.Render()
                    StiWebViewer1.Report = report
                End If
            ElseIf Request.QueryString("type") = "invoice" Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim report As StiReport = New StiReport()
                    report.Load(Server.MapPath("~/Reports/Invoice.mrt"))
                    report.Compile()
                    report("id") = Request.QueryString("id").ToString()
                    report.Render()
                    StiWebViewer1.Report = report
                End If
            End If

        End If
    End Sub

End Class