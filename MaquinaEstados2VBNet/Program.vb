Imports System
Imports System.Data
Imports MongoDB.Bson
Imports MongoDB.Driver

Module Program

    'Private myConn As SqlConnection
    'Private myCmd As SqlCommand
    'Private myReader As SqlDataReader
    Public results As String
    Public sql_instruction As String
    Public machine As String = "DESKTOP-FAJHIAM"
    Public robot As String = "RoborPuebasLeo"
    Public Declare Sub Sleep Lib "kernel32" Alias "Sleep" (ByVal dwMilliseconds As Long)
    Dim LOGMESSAGE_TRANSITION_SYSTEM_EXCEPTION As String = "SYSTEM_EXCEPTION"
    Dim LOGMESSAGE_STATE_PROCESS_FINISHED As String = "PROCESS_FINISHED"
    Dim LOGMESSAGE_STATE_TRANSACTION_IN_PROGRESS As String = "DATA_LOADED"
    Dim LOGMESSAGE_TRANSITION_SUCCESSFUL As String = "SUCCESSFUL"
    Dim LOGMESSAGE_TRANSITION_NEW_TRANSACTION As String = "LOADING_TRANSACTION_DATA"
    Dim LOGMESSAGE_APPLICATION_EXCEPTION As String = "SYSTEM_EXCEPTION"
    Dim LOGMESSAGE_KILLING_PROCESSES As String = "KILLING_PROCESSES"
    Dim LOGMESSAGE_OPENING_APPLICATIONS As String = "OPENING_APPLICATIONS"
    Dim LOGMESSAGE_CLOSING_APPLICATIONS As String = "Closing_applications"
    Dim LOGMESSAGE_LOADING_TRANSACTION_DATA As String = "LOADING_TRANSACTION_DATA"
    Dim LOGMESSAGE_TRANSACTION_DATA_LOADED As String = "LOADING_TRANSACTION_DATA"
    Dim LOGMESSAGE_SUCCESS As String = "SUCCESSFUL"
    Dim LOGMESSAGE_AUTOMATION_STARTED As String = "AUTOMATION_STARTED"

    Dim CONFIG As Dictionary(Of String, String) = New Dictionary(Of String, String)
    ' Create new DataTable instance.
    Dim table As New DataTable
    Dim client As MongoClient
    Dim db As IMongoDatabase

    Sub Main(args As String())
        'NewtonsoftJsonSerializer
        Dim i = 0
        'initialization
        'Read Configuration file And initialize applications used in the process.
        Dim transition As String
        transition = initialize()
        If transition = LOGMESSAGE_TRANSITION_SUCCESSFUL Then
            transition = load_transaction_data()
            If transition = LOGMESSAGE_TRANSACTION_DATA_LOADED Then

                For Each row As DataRow In table.Rows
                    Dim sGUID As String
                    sGUID = System.Guid.NewGuid.ToString()
                    Try
                        save_log("Trace", LOGMESSAGE_TRANSITION_NEW_TRANSACTION, LOGMESSAGE_TRANSITION_NEW_TRANSACTION, sGUID, "", "Id: " & row("Id") & " Name: " & row("Name") & " Date: " & row("Date"))
                        Sleep(1000)
                        save_log("Trace", LOGMESSAGE_STATE_TRANSACTION_IN_PROGRESS, LOGMESSAGE_STATE_TRANSACTION_IN_PROGRESS, sGUID)
                        Sleep(3000)
                        i = i + 1
                        If i = 2 Then
                            Throw New System.Exception
                        End If
                        save_log("Trace", LOGMESSAGE_SUCCESS, LOGMESSAGE_SUCCESS, sGUID)
                        Sleep(1000)
                    Catch ex As Exception
                        save_log("Trace", LOGMESSAGE_TRANSITION_SYSTEM_EXCEPTION, LOGMESSAGE_TRANSITION_SYSTEM_EXCEPTION, sGUID, ex.Message)
                        'Reintentar MAX_RETRY_NUMBER veces
                    End Try

                Next row
            End If
        Else
            end_process()
        End If
        end_process()
    End Sub


    'initialization
    'Read configuration file And initialize applications used in the process.
    Public Function initialize() As String
        'Try initializing settings And applications
        Try
            'Load configurations And open applications
            'If first Then run, read configuration
            If CONFIG.Count = 0 Then
                initialize_all_settings()
                Sleep(1000)
                kill_all_processes()
                Sleep(1000)
            Else
            End If
            init_all_applications()
            Sleep(3000)
            initialize = LOGMESSAGE_TRANSITION_SUCCESSFUL
        Catch ex As Exception
            initialize = LOGMESSAGE_APPLICATION_EXCEPTION
        End Try
    End Function

    Public Sub save_log(ByVal LogLevel As String, ByVal state As String, ByVal Message As String, Optional TransactionNumber As String = "", Optional Description As String = "", Optional payload As String = "")
        Try

            client = New MongoClient("mongodb+srv://carlos:ioIey8a3QPVBLaqg@cluster0.c2gut.mongodb.net/ecomplex")
            db = client.GetDatabase("ecomplex")

            Dim collection As IMongoCollection(Of BsonDocument) = db.GetCollection(Of BsonDocument)("log_robots")

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'CREATE
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim log_robots As BsonDocument = New BsonDocument
            With log_robots
                .Add("_id", Guid.NewGuid().ToString) 'Guid.NewGuid().ToString)
                .Add("machine", machine)
                .Add("robot", robot)
                .Add("date", DateTime.Now)
                .Add("log_level", "Trace")
                .Add("message", Message)
                .Add("transaction_number", TransactionNumber)
                .Add("description", Description)
                .Add("payload", payload)
                .Add("state", state)

            End With
            collection.InsertOne(log_robots)

            Dim Log_Message As String = ""
            Log_Message = "Trace " & LogLevel & " " & " Message " & Message
            If Description <> "" Then
                Log_Message = Log_Message & " Description " & Description
            End If
            If payload <> "" Then
                Log_Message = Log_Message & " Payload " & payload
            End If
            Console.WriteLine(Log_Message)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    'Initialize, populate And output the configuration values to be used throughout the project.
    Public Sub initialize_all_settings()
        save_log("Trace", LOGMESSAGE_AUTOMATION_STARTED, LOGMESSAGE_AUTOMATION_STARTED)
    End Sub



    'Use the Kill Process activity to force the termination of  processes representing applications used in the business process being automated.
    'Note that killing processes might have undesirable outcomes, such as losing unsaved changes to files.    
    Public Sub kill_all_processes()
        'TODO Code to force the termination of  processes representing applications used in the business process being automated
        save_log("Trace", LOGMESSAGE_KILLING_PROCESSES, LOGMESSAGE_KILLING_PROCESSES)
    End Sub

    'Open applications used in the process and do necessary initialization procedures (e.g., login).
    Public Sub init_all_applications()
        'TODO: Code to open applications used in the business process being automated
        save_log("Trace", LOGMESSAGE_OPENING_APPLICATIONS, LOGMESSAGE_OPENING_APPLICATIONS)
    End Sub

    'Do the necessary procedures for ending the process (e.g., logout) And close the used applications.
    Public Sub close_all_applications()
        Try
            'TODO: Code to open applications used in the business process being automated
            save_log("Trace", LOGMESSAGE_CLOSING_APPLICATIONS, LOGMESSAGE_CLOSING_APPLICATIONS)
            Sleep(2000)
        Catch ex As Exception
            kill_all_processes()
            Sleep(1000)
        End Try
    End Sub

    'Do the necessary procedures for ending the process (e.g., logout) And close the used applications.
    Public Sub end_process()
        close_all_applications()
        'TODO: Code to open applications used in the business process being automated
        save_log("Trace", LOGMESSAGE_STATE_PROCESS_FINISHED, LOGMESSAGE_STATE_PROCESS_FINISHED)
    End Sub

    Function GetTable() As DataTable
        Return table
    End Function

    'Get all transaction items from a specified source (e.g., queues, spreadsheets, databases, mailboxes, web APIs Or any other)
    Public Function load_transaction_data() As String
        Try
            save_log("Trace", LOGMESSAGE_LOADING_TRANSACTION_DATA, LOGMESSAGE_LOADING_TRANSACTION_DATA)
            ' Create four typed columns in the DataTable.
            table.Columns.Add("Id", GetType(Integer))
            table.Columns.Add("Name", GetType(String))
            table.Columns.Add("Date", GetType(DateTime))
            ' Add five rows with those columns filled in the DataTable.
            table.Rows.Add(25, "Juan", DateTime.Now)

            Dim lname() As String
            Dim rand As New Random()
            Dim ranLname As String
            Dim i As Integer = 0
            lname = IO.File.ReadAllLines("C:\random_names\random_names.txt")


            For i = 1 To 10
                ranLname = lname(rand.Next(0, lname.Length - 1))
                table.Rows.Add(rand.Next(0, 1000), ranLname, DateTime.Now)
                'table.Rows.Add(10, "Christoff", DateTime.Now)
                'table.Rows.Add(21, "Janet", DateTime.Now)
                'table.Rows.Add(100, "Melanie", DateTime.Now)
            Next
            load_transaction_data = LOGMESSAGE_TRANSACTION_DATA_LOADED
        Catch ex As Exception
            '...handle exception...
            load_transaction_data = LOGMESSAGE_APPLICATION_EXCEPTION
        End Try
    End Function
End Module

