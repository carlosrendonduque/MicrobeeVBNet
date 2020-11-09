Imports System
Imports System.Net.Http
Imports System.Runtime.Remoting.Messaging
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json

Module Program

    Dim LOGMESSAGE_STATE_TRANSACTION_PROCESSED As String = "Transaction Processed"
    Dim LOGMESSAGE_STATE_PROCESS_FINISHED As String = "Process Finished"
    Dim LOGMESSAGE_STATE_TRANSACTION_IN_PROGRESS As String = "Transaction in Progress"
    Dim LOGMESSAGE_TRANSITION_SUCCESSFUL As String = "Successful"
    Dim LOGMESSAGE_TRANSITION_NEW_TRANSACTION As String = "New Transaction"
    Dim LOGMESSAGE_TRANSITION_BUSINESS_EXCEPTION As String = "Business Exception"
    Dim LOGMESSAGE_TRANSITION_SYSTEM_EXCEPTION As String = "System Exception"
    Dim LOGMESSAGE_TRANSITION_SUCCESS As String = "Success"
    Dim LOGMESSAGE_TRANSITION_NO_DATA As String = "No Data"
    Dim MICROBOT_ENVIRONMENT As String = "production"
    Dim LOGMESSAGE_AUTOMATION_STARTED As String = "Automation Started"
    Dim ORCHESTRATOR_QUEUE_NAME As String = "orchestrator_queue_name"
    Dim QUEUE_RABBIT_PUBLISHER As String = "queue_test_microbot"
    Dim QUEUE_RABBIT_CONSUMER As String = "queue_test_microbot"
    Dim LOGF_BUSINESS_PROCESS_NAME As String = "framework"
    Dim MAX_RETRY_NUMBER As Double = 0
    Dim LOGMESSAGE_GET_TRANSACTION_DATA As String = "Exception calling transaction number"
    Dim LOGMESSAGE_GET_TRANSACTION_DATA_ERROR As String = "Error getting transaction number"
    Dim LOGMESSAGE_SUCCESS As String = "Transaction Successful"
    Dim LOGMESSAGE_BUSINESS_RULE_EXCEPTION As String = "Business Rule Exception"
    Dim LOGMESSAGE_APPLICATION_EXCEPTION As String = "System Exception"
    Dim LOGMESSAGE_KILLING_PROCESSES As String = "Killing processes"
    Dim LOGMESSAGE_OPENING_APPLICATIONS As String = "Opening applications"
    Dim LOGMESSAGE_CLOSING_APPLICATIONS As String = "Closing applications"
    Dim LOGMESSAGE_LOADING_TRANSACTION_DATA As String = "Loading transaction data"
    Dim LOGMESSAGE_TRANSACTION_DATA_LOADED As String = "Loading transaction data"
    Dim RABBIT_CONNECTION As String = "amqp://localhost"
    Dim EXTERNAL_SETTINGS_API As String = "http://localhost"
    Dim EXTERNAL_CONFIG_KEY As String = ""

    Dim JSONString As String
    Dim CONFIG As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Dim LOG_PROCESS As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Dim TRANSITIONS As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Dim STATES As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Dim trm As Task(Of HttpResponseMessage)
    Dim rm As HttpResponseMessage
    Dim ts As Task(Of String)
    ' Create new DataTable instance.
    Dim table As New DataTable


    Sub Main(args As String())

        'initialization
        'Read Configuration file And initialize applications used in the process.
        Dim transition As String
        transition = initialize()
        If transition = LOGMESSAGE_TRANSITION_SUCCESSFUL Then
            transition = load_transaction_data()
            If transition = LOGMESSAGE_TRANSACTION_DATA_LOADED Then

                For Each row As DataRow In table.Rows
                    Try
                        save_log("Trace", LOGMESSAGE_TRANSITION_NEW_TRANSACTION)
                        save_log("Trace", LOGMESSAGE_STATE_TRANSACTION_IN_PROGRESS)
                        save_log("Trace", "Id: " & row("Id") & " Name: " & row("Name") & " Date: " & row("Date"))
                        save_log("Trace", LOGMESSAGE_SUCCESS)
                    Catch ex As Exception
                        save_log("Trace", LOGMESSAGE_TRANSITION_SYSTEM_EXCEPTION)
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
                kill_all_processes()
            Else
            End If
            init_all_applications()
            initialize = LOGMESSAGE_TRANSITION_SUCCESSFUL
        Catch ex As Exception
            initialize = LOGMESSAGE_APPLICATION_EXCEPTION
        End Try
    End Function

    Public Sub save_log(ByVal LogLevel As String, ByVal Message As String)
        Console.WriteLine("LogLevel: " & LogLevel & " / " & "Message: " & Message)
    End Sub

    'Initialize, populate And output the configuration values to be used throughout the project.
    Public Sub initialize_all_settings()

        'CONFIG.Add("QUEUE_RABBIT_PUBLISHER", QUEUE_RABBIT_PUBLISHER)
        'CONFIG.Add("QUEUE_RABBIT_CONSUMER", QUEUE_RABBIT_CONSUMER)
        'CONFIG.Add("LOGF_BUSINESS_PROCESS_NAME", LOGF_BUSINESS_PROCESS_NAME)
        'CONFIG.Add("MAX_RETRY_NUMBER", MAX_RETRY_NUMBER)
        'CONFIG.Add("LOGMESSAGE_GET_TRANSACTION_DATA", LOGMESSAGE_GET_TRANSACTION_DATA)
        'CONFIG.Add("LOGMESSAGE_GET_TRANSACTION_DATA_ERROR", LOGMESSAGE_GET_TRANSACTION_DATA_ERROR)
        'CONFIG.Add("LOGMESSAGE_SUCCESS", LOGMESSAGE_SUCCESS)
        'CONFIG.Add("LOGMESSAGE_BUSINESS_RULE_EXCEPTION", LOGMESSAGE_BUSINESS_RULE_EXCEPTION)
        'CONFIG.Add("LOGMESSAGE_APPLICATION_EXCEPTION", LOGMESSAGE_APPLICATION_EXCEPTION)
        'CONFIG.Add("RABBIT_CONNECTION", RABBIT_CONNECTION)
        'CONFIG.Add("QUEUE_RABBIT_PUBLISHER", QUEUE_RABBIT_PUBLISHER)
        'CONFIG.Add("ORCHESTRATOR_QUEUE_NAME", ORCHESTRATOR_QUEUE_NAME)
        'CONFIG.Add("EXTERNAL_SETTINGS_API", EXTERNAL_SETTINGS_API)
        'CONFIG.Add("EXTERNAL_CONFIG_KEY", EXTERNAL_CONFIG_KEY)

        save_log("Trace", LOGMESSAGE_AUTOMATION_STARTED)

    End Sub


    'Use the Kill Process activity to force the termination of  processes representing applications used in the business process being automated.
    'Note that killing processes might have undesirable outcomes, such as losing unsaved changes to files.    
    Public Sub kill_all_processes()

        'TODO Code to force the termination of  processes representing applications used in the business process being automated
        save_log("Trace", LOGMESSAGE_KILLING_PROCESSES)

    End Sub

    'Open applications used in the process and do necessary initialization procedures (e.g., login).
    Public Sub init_all_applications()

        'TODO: Code to open applications used in the business process being automated
        save_log("Trace", LOGMESSAGE_OPENING_APPLICATIONS)

    End Sub



    'Do the necessary procedures for ending the process (e.g., logout) And close the used applications.
    Public Sub close_all_applications()
        Try
            'TODO: Code to open applications used in the business process being automated
            save_log("Trace", LOGMESSAGE_CLOSING_APPLICATIONS)
        Catch ex As Exception
            kill_all_processes()
        End Try

    End Sub

    'Do the necessary procedures for ending the process (e.g., logout) And close the used applications.
    Public Sub end_process()
        close_all_applications()
        'TODO: Code to open applications used in the business process being automated
        save_log("Trace", LOGMESSAGE_STATE_PROCESS_FINISHED)

    End Sub
    Function GetTable() As DataTable

        Return table
    End Function

    'Get all transaction items from a specified source (e.g., queues, spreadsheets, databases, mailboxes, web APIs Or any other)
    Public Function load_transaction_data() As String


        Try

            save_log("Trace", LOGMESSAGE_LOADING_TRANSACTION_DATA)


            ' Create four typed columns in the DataTable.
            table.Columns.Add("Id", GetType(Integer))
            table.Columns.Add("Name", GetType(String))
            table.Columns.Add("Date", GetType(DateTime))

            ' Add five rows with those columns filled in the DataTable.
            table.Rows.Add(25, "Juan", DateTime.Now)
            table.Rows.Add(50, "David", DateTime.Now)
            table.Rows.Add(10, "Christoff", DateTime.Now)
            table.Rows.Add(21, "Janet", DateTime.Now)
            table.Rows.Add(100, "Melanie", DateTime.Now)

            load_transaction_data = LOGMESSAGE_TRANSACTION_DATA_LOADED
        Catch ex As Exception
            '...handle exception...
            load_transaction_data = LOGMESSAGE_APPLICATION_EXCEPTION
        End Try
    End Function
End Module
