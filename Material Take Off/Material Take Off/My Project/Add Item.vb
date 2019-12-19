Imports System.Windows.Forms
Imports System.Data.SQLite
Imports System.Data


Public Class Add_Item
    Dim SaveProjDb As New SaveFileDialog
    Dim OpenProjDb As New OpenFileDialog
    Dim SQLconn As SQLiteConnection

    Private Sub CreateProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateProjectToolStripMenuItem.Click
        SaveProjDb.Title = "Save New Project"
        SaveProjDb.Filter = ".db|*.db"
        SaveProjDb.ShowDialog()

        Dim connectionString As String
        connectionString = "Data Source={0};Version=3;"
        Dim configDb As String = SaveProjDb.FileName.ToString

        'connecting to db
        connectionString = String.Format(connectionString, configDb)

        'Creating db file
        SQLiteConnection.CreateFile(configDb)

        Me.Text = "Add Item (" & configDb & ")"
        Me.Refresh()

        SQLconn = New SQLiteConnection(connectionString)

        Dim create_Tab As String = String.Empty

        create_Tab &= "CREATE TABLE [Size] (
                      [Id] INTEGER NOT NULL
                    , [Size] INTEGER NOT NULL
                    , CONSTRAINT [PK_Size] PRIMARY KEY ([Id])
                    );"
        SQLconn.Open()

        Try
            Using cmd As New SQLiteCommand(create_Tab, SQLconn)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("Size Table is not created.")
        End Try
        AddSizeData()

        create_Tab = String.Empty
        create_Tab &=
                "CREATE TABLE [Spec] (
          [Id] INTEGER NOT NULL
        , [Spec] TEXT NOT NULL
        , CONSTRAINT [PK_Spec] PRIMARY KEY ([Id])
        );"

        Try
            Using cmd As New SQLiteCommand(create_Tab, SQLconn)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MessageBox.Show("Spec Table is not created.")
        End Try

        create_Tab = String.Empty
        create_Tab &= "CREATE TABLE [InsSize] (
                          [Id] INTEGER NOT NULL
                        , [Size] INTEGER NOT NULL
                        , CONSTRAINT [PK_InsSize] PRIMARY KEY ([Id])
                        );"
        Try
            Using cmd As New SQLiteCommand(create_Tab, SQLconn)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("InsSize Table is not created.")
        End Try

        create_Tab = String.Empty
        create_Tab &= "CREATE TABLE [InsTyp] (
                      [Id] INTEGER NOT NULL
                    , [Typ] TEXT NOT NULL
                    , CONSTRAINT [PK_InsTyp] PRIMARY KEY ([Id])
                    );"
        Try
            Using cmd As New SQLiteCommand(create_Tab, SQLconn)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("InsTyp Table is not created.")
        End Try


        AddInsulationData()


        create_Tab = String.Empty
        create_Tab &= "CREATE TABLE [Valve] (
                      [Id] INTEGER NOT NULL
                    , [Valve] TEXT NOT NULL
                    , CONSTRAINT [PK_Valve] PRIMARY KEY ([Id])
                    );"
        Try
            Using cmd As New SQLiteCommand(create_Tab, SQLconn)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("Valve Table is not created.")
        End Try

        AddValveData()

        create_Tab = String.Empty
        create_Tab &= "CREATE TABLE [PID] (
                      [Id] INTEGER NOT NULL
                    , [PID_NO] TEXT NULL
                    , CONSTRAINT [PK_PID] PRIMARY KEY ([Id])
                    );"
        Try
            Using cmd As New SQLiteCommand(create_Tab, SQLconn)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("PID No. Table is not created.")
        End Try

        create_Tab = String.Empty
        create_Tab &= "CREATE TABLE [LineWiseMTO] (
                      [Id] INTEGER NOT NULL
                    , [Line No] INTEGER NOT NULL
                    , [Spec] TEXT NOT NULL
                    , [Main Size] INTEGER NOT NULL
                    , [Reduced Size] INTEGER NULL
                    , [Component] TEXT NOT NULL
                    , [Component Code] INTEGER NOT NULL
                    , [Quantity] NUMERIC NOT NULL
                    , [Remarks] TEXT NULL
                    , [Insulation Type] TEXT NULL
                    , [Insulation Thickness] INTEGER NULL
                    , CONSTRAINT [PK_LineWiseMTO] PRIMARY KEY ([Id])
                    );"
        Try
            Using cmd As New SQLiteCommand(create_Tab, SQLconn)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("LineWiseMTO Table is not created.")
        End Try

    End Sub

    Private Sub OpenProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenProjectToolStripMenuItem.Click
        OpenProjDb.Title = "Open Project"
        OpenProjDb.Filter = ".db|*.db"
        OpenProjDb.ShowDialog()
        Dim connectionString As String = "Data Source={0};Version=3;"
        Dim configDb As String = OpenProjDb.FileName.ToString

        'connecting to db
        connectionString = String.Format(connectionString, configDb)

        SQLconn = New SQLiteConnection(connectionString)

        Me.Text = "Add Item (" & configDb & ")"
        Me.Refresh()

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        SQLconn.Dispose()
        SQLconn = Nothing
        Application.Exit()
    End Sub



    Sub AddSizeData()
        Dim i As Integer
        Dim Size(40) As Integer
        Dim addSize As String

        Size = New Integer() {6, 8, 10, 15, 20, 25, 32, 40, 50,
            65, 80, 90, 100, 125, 150, 200, 250, 300, 350, 400,
            450, 500, 550, 600, 700, 800, 900, 1000, 1200, 1400, 1600,
            1800, 2000, 2200, 2400, 2600, 2800, 3000, 3200, 3400, 3600}

        For i = 0 To (Size.Length - 1)
            addSize = String.Empty
            addSize &= "INSERT INTO [Size]
              ([Id]
              ,[Size])
        VALUES
              (" & i & "," & Size(i) & ");"

            Console.WriteLine(addSize.ToString)

            Try
                Using cmd As New SQLiteCommand(addSize, SQLconn)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show("Size Data Adding Error")
            End Try
        Next
    End Sub

    Sub AddInsulationData()
        Dim i As Integer
        Dim typ(4) As String
        typ = New String() {"-", "IP", "C", "H"}
        Dim thk(8) As Integer
        thk = New Integer() {0, 25, 30, 40, 50, 60, 70, 80}
        Dim InsertQry As String
        InsertQry = String.Empty

        For i = 0 To (typ.Length - 1)
            InsertQry = String.Empty
            InsertQry &= "INSERT INTO [InsTyp]
                       ([Id]
                       ,[Typ])
                 VALUES
                       (" & i & ",'" & typ(i) & "');"
            'Console.WriteLine(InsertQry.ToString)

            Try
                Using cmd As New SQLiteCommand(InsertQry, SQLconn)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show("AddInsTyp Data Adding Error")
            End Try
        Next


        For i = 0 To (thk.Length - 1)
            InsertQry = String.Empty
            InsertQry &= "INSERT INTO [InsSize]
                           ([Id]
                           ,[Size])
                     VALUES
                           (" & i & "," & thk(i) & ");"
            Try
                Using cmd As New SQLiteCommand(InsertQry, SQLconn)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show("AddInsThk Data Adding Error")
            End Try
        Next

    End Sub

    Private Sub AddValveData()

        Dim i As Integer
        Dim valve(7) As String
        valve = New String() {"Ball Valve", "Globe Valve", "Gate Valve",
            "Angle Valve", "Check Valve", "Piston Valve", "Butterfly Valve"}

        Dim InsertQry As String
        InsertQry = String.Empty

        For i = 0 To (valve.Length - 1)
            InsertQry = String.Empty
            InsertQry &= "INSERT INTO [Valve]
                       ([Id]
                       ,[Valve])
                 VALUES
                       (" & i & ",'" & valve(i) & "');"
            'Console.WriteLine(InsertQry.ToString)

            Try
                Using cmd As New SQLiteCommand(InsertQry, SQLconn)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show("AddInsTyp Data Adding Error")
            End Try
        Next

    End Sub

    Private Sub SIZEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SIZEToolStripMenuItem.Click
        Dim SizeForm As New Table_View With {
            .Text = "Size"
        }

        Dim SizeQry As String

        SizeQry = String.Empty
        SizeQry &= "SELECT [Id]
                  ,[Size]
              FROM [Size] LIMIT 200"

        SizeForm.SQLConn = SQLconn
        SizeForm.Qry = SizeQry

        SizeForm.Show()
        'SizeForm.DataGridView1.Columns("Id").ReadOnly = True

    End Sub

    Private Sub SPECToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SPECToolStripMenuItem.Click
        Dim SpecForm As New Table_View With {
            .Text = "Specifications"
        }

        Dim SpecQry As String

        SpecQry = String.Empty
        SpecQry &= "SELECT [Id]
                  ,[Spec]
                  FROM [Spec] LIMIT 200"

        SpecForm.SQLConn = SQLconn
        SpecForm.Qry = SpecQry

        SpecForm.Show()

        'SizeForm.DataGridView1.Columns("Id").ReadOnly = True


    End Sub

    Private Sub INSULATION_TYP_ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles INSULATION_TYP_ToolStripMenuItem.Click
        Dim InsTypForm As New Table_View With {
            .Text = "Insulation Type"
        }

        Dim InsTypQry As String

        InsTypQry = String.Empty
        InsTypQry &= "SELECT [Id]
                      ,[Typ]
                  FROM [InsTyp] LIMIT 200"

        InsTypForm.SQLConn = SQLconn
        InsTypForm.Qry = InsTypQry
        InsTypForm.Show()

        'SizeForm.DataGridView1.Columns("Id").ReadOnly = True

    End Sub

    Private Sub INS_THK_ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles INS_THK_ToolStripMenuItem.Click
        Dim InsThkForm As New Table_View With {
            .Text = "Insulation Thickness"
        }

        Dim InsThkQry As String

        InsThkQry = String.Empty
        InsThkQry &= "SELECT [Id]
                      ,[Size]
                  FROM [InsSize] LIMIT 200"

        InsThkForm.SQLConn = SQLconn
        InsThkForm.Qry = InsThkQry

        InsThkForm.Show()

        'SizeForm.DataGridView1.Columns("Id").ReadOnly = True

    End Sub

    Private Sub PIDNOToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PIDNOToolStripMenuItem1.Click
        Dim PIDForm As New Table_View With {
            .Text = "PID No."
        }

        Dim PIDQry As String

        PIDQry = String.Empty
        PIDQry &= "SELECT [Id]
                  ,[PID_NO]
              FROM [PID] LIMIT 200"

        PIDForm.SQLConn = SQLconn
        PIDForm.Qry = PIDQry

        PIDForm.Show()

    End Sub

    Private Sub VALVETYPEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VALVETYPEToolStripMenuItem.Click
        Dim ValveForm As New Table_View With {
            .Text = "Valve Type"
        }

        Dim ValveQry As String

        ValveQry = String.Empty
        ValveQry &= "SELECT [Id]
                    ,[Valve]
                FROM [Valve] LIMIT 200"

        ValveForm.SQLConn = SQLconn
        ValveForm.Qry = ValveQry

        ValveForm.Show()

    End Sub

    Private Sub Add_Item_Load(sender As Object, e As EventArgs) Handles Me.Load
        clearButton()

    End Sub

    Sub clearButton()
        TextBox2.Text = "0"
        TextBox3.Text = "0"
        TextBox4.Text = "0"
        TextBox5.Text = "0"
        TextBox6.Text = "0"
        TextBox7.Text = "0"
        TextBox8.Text = "0"
        TextBox9.Text = "0"
        TextBox12.Text = "0"
        TextBox13.Text = "0"
        TextBox15.Text = "0"
        TextBox16.Text = "0"
        TextBox18.Text = "0"
        TextBox19.Text = "0"
        TextBox21.Text = "0"
        TextBox22.Text = "0"
        TextBox24.Text = "0"
        TextBox25.Text = "0"
        TextBox27.Text = "0"
        TextBox28.Text = "0"
        TextBox29.Text = "0"
        TextBox30.Text = "0"
        TextBox31.Text = "0"
        TextBox32.Text = "0"
        TextBox33.Text = "0"
        TextBox34.Text = "0"
        TextBox35.Text = "0"
        TextBox36.Text = "0"
        TextBox37.Text = "0"
        TextBox38.Text = "0"
        TextBox39.Text = "0"
        TextBox40.Text = "0"
        TextBox41.Text = "0"
        TextBox42.Text = "0"


    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox11.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox12_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox12.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox13.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox14_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox14.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox15_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox15.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox16_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox16.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox17_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox17.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox18_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox18.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox19_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox19.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox20_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox20.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox21_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox21.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox22_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox22.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox23_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox23.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox24_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox24.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox25_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox25.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox26_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox26.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox27_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox27.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox28_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox28.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox29_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox29.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox30_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox30.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox31_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox31.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox32_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox32.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox33_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox33.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox34_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox34.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox35_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox35.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox36_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox36.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox37_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox37.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox38_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox38.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox39_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox39.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox40_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox40.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox41_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox41.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox42_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox42.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress
        Dim allowedChar As String = "0123456789"
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim allowedChar As String = "0123456789."
        If allowedChar.IndexOf(e.KeyChar) = -1 Then
            e.Handled = True
        End If
    End Sub
End Class