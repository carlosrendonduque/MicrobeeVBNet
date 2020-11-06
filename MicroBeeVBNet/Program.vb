Imports System

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

    Dim CONFIG As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Dim LOG_PROCESS As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Dim TRANSITIONS As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Dim STATES As Dictionary(Of String, String) = New Dictionary(Of String, String)



    Sub Main(args As String())
        Console.WriteLine("Hello World!")

        'initialization
        'Read Configuration file And initialize applications used in the process.
        Dim transition As String
        transition = initialize()


    End Sub

    Public Function initialize() As String
        'Try initializing settings And applications
        Try

            'Load configurations And open applications
            'If first Then run, read configuration
            If IsNothing(CONFIG) Then

            Else

            End If
            LOG_PROCESS.Add("AUTOMATION_STARTED", "")
            LOG_PROCESS.Add("KILLING_PROCESSES", "")
            LOG_PROCESS.Add("OPENING_APPLICATIONS", "")
            LOG_PROCESS.Add("CLOSING_APPLICATIONS", "")
            LOG_PROCESS.Add("LOADING_TRANSACTION_DATA", "")


            TRANSITIONS.Add("SUCCESSFUL", "")
            TRANSITIONS.Add("NEW_TRANSACTION", "")
            TRANSITIONS.Add("BUSINESS_EXCEPTION", "")
            TRANSITIONS.Add("SYSTEM_EXCEPTION", "")
            TRANSITIONS.Add("SUCCESS", "")
            TRANSITIONS.Add("NO_DATA", "")
            TRANSITIONS.Add("LOADING_TRANSACTION_DATA", "")

            STATES.Add("PROCESS_INITIALIZED", "")
            STATES.Add("DATA_LOADED", "")
            STATES.Add("TRANSACTION_PROCESSED", "")
            STATES.Add("PROCESS_FINISHED", "")

            LOG_PROCESS.Add("AUTOMATION_STARTED", LOGMESSAGE_AUTOMATION_STARTED)

            save_log("Trace", LOG_PROCESS.Item("AUTOMATION_STARTED")

            initAllSetObj.initialize_all_settings()
            KillAllProObj.kill_all_processes()

            Console.WriteLine("Hello World2!")
            Return ""
        Catch ex As Exception

        End Try

    End Function

    Public Sub save_log(ByVal LogLevel As String, ByVal Message As String)
        Console.WriteLine("LogLevel: " & LogLevel & " / " & "Message: " & Message)
    End Sub

    Public Sub initialize_all_settings()
        Global.CONFIG['QUEUE_RABBIT_PUBLISHER'] = process.env.QUEUE_RABBIT_PUBLISHER;
        Global.CONFIG['QUEUE_RABBIT_CONSUMER'] = process.env.QUEUE_RABBIT_CONSUMER;
        Global.CONFIG['LOGF_BUSINESS_PROCESS_NAME'] = process.env.LOGF_BUSINESS_PROCESS_NAME;
        Global.CONFIG['MAX_RETRY_NUMBER'] = process.env.MAX_RETRY_NUMBER;
        Global.CONFIG['LOGMESSAGE_GET_TRANSACTION_DATA'] = process.env.LOGMESSAGE_GET_TRANSACTION_DATA;
        Global.CONFIG['LOGMESSAGE_GET_TRANSACTION_DATA_ERROR'] = process.env.LOGMESSAGE_GET_TRANSACTION_DATA_ERROR;
        Global.CONFIG['LOGMESSAGE_SUCCESS'] = process.env.LOGMESSAGE_SUCCESS;
        Global.CONFIG['LOGMESSAGE_BUSINESS_RULE_EXCEPTION'] = process.env.LOGMESSAGE_BUSINESS_RULE_EXCEPTION;
        Global.CONFIG['LOGMESSAGE_APPLICATION_EXCEPTION'] = process.env.LOGMESSAGE_APPLICATION_EXCEPTION;
        Global.CONFIG['RABBIT_CONNECTION'] = process.env.RABBIT_CONNECTION;
        Global.CONFIG['QUEUE_RABBIT_PUBLISHER'] = process.env.QUEUE_RABBIT_PUBLISHER;
        Global.CONFIG['ORCHESTRATOR_QUEUE_NAME'] = process.env.ORCHESTRATOR_QUEUE_NAME;
        Global.CONFIG['EXTERNAL_SETTINGS_API'] = process.env.EXTERNAL_SETTINGS_API;
        CONFIG.Add("QUEUE_RABBIT_PUBLISHER", "")
        CONFIG.Add("QUEUE_RABBIT_CONSUMER", "")
        CONFIG.Add("LOGF_BUSINESS_PROCESS_NAME", "")
        CONFIG.Add("MAX_RETRY_NUMBER", "")
        CONFIG.Add("LOGMESSAGE_GET_TRANSACTION_DATA", "")
        CONFIG.Add("LOADING_TRANSACTION_DATA", "")
        CONFIG.Add("LOADING_TRANSACTION_DATA", "")
        CONFIG.Add("LOADING_TRANSACTION_DATA", "")
        CONFIG.Add("LOADING_TRANSACTION_DATA", "")
        CONFIG.Add("LOADING_TRANSACTION_DATA", "")
        CONFIG.Add("LOADING_TRANSACTION_DATA", "")
        CONFIG.Add("LOADING_TRANSACTION_DATA", "")
        CONFIG.Add("LOADING_TRANSACTION_DATA", "")
        CONFIG.Add("LOADING_TRANSACTION_DATA", "")
        CONFIG.Add("LOADING_TRANSACTION_DATA", "")

    End Sub
End Module
