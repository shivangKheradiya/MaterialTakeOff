
Imports System.Windows.Forms
Imports System.Data.SQLite
Imports System.Data
Imports System.ComponentModel

Public Class Table_View
    Dim id As Integer
    Dim rowCount As Integer
    Public SQLConn As SQLiteConnection


    Public Qry As String
    Public Form_ds As New DataSet
    Public Form_da As SQLiteDataAdapter

    Dim builder As SQLiteCommandBuilder

    'Private Sub DataGridView1_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView1.UserAddedRow
    '    'MsgBox(DataGridView1.Rows.Count - 2)
    '    e.Row.Cells(0).Value = DataGridView1.Rows.Count - 1
    'End Sub

    'Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
    '    DataGridView1.Rows(e.RowIndex).Cells(0).Value = DataGridView1.Rows.Count - 1
    'End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        If DataGridView1.Rows(e.RowIndex).Cells(0).Value Is Nothing Then
            DataGridView1.Rows(e.RowIndex).Cells(0).Value = id + 1
        End If
    End Sub

    Private Sub Table_View_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        DataGridView1.Columns("Id").ReadOnly = True
        rowCount = DataGridView1.Rows.Count
        id = 0
        Dim i As Integer
        i = 0
        While i < rowCount
            If id < DataGridView1.Rows(i).Cells(0).Value Then
                id = DataGridView1.Rows(i).Cells(0).Value
            End If
            i += 1
        End While

    End Sub

    Private Sub DataGridView1_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView1.UserAddedRow
        id += 1
    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        'MsgBox(DataGridView1.DataSource.ToString)
        If Form_ds.GetChanges IsNot Nothing Then
            Form_da.Update(Form_ds.GetChanges)
        End If
    End Sub

    Private Sub Table_View_Load(sender As Object, e As EventArgs) Handles Me.Load
        Form_da = New SQLiteDataAdapter(Qry, SQLConn)
        Form_da.Fill(Form_ds)
        builder = New SQLiteCommandBuilder(Form_da)
        Me.DataGridView1.DataSource = Form_ds.Tables(0).DefaultView
    End Sub

    Private Sub Table_View_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Form_da.Dispose()
        Form_ds.Dispose()

    End Sub
End Class