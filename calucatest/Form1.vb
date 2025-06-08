Public Class Form1
    Private input1 As String = ""
    Private input2 As String = ""
    Private operation As String = ""
    Private isSecondInput As Boolean = False
    Private resultDisplayed As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 数字ボタン
        AddHandler Button0.Click, AddressOf NumberButton_Click
        AddHandler Button1.Click, AddressOf NumberButton_Click
        AddHandler Button2.Click, AddressOf NumberButton_Click
        AddHandler Button3.Click, AddressOf NumberButton_Click
        AddHandler Button4.Click, AddressOf NumberButton_Click
        AddHandler Button5.Click, AddressOf NumberButton_Click
        AddHandler Button6.Click, AddressOf NumberButton_Click
        AddHandler Button7.Click, AddressOf NumberButton_Click
        AddHandler Button8.Click, AddressOf NumberButton_Click
        AddHandler Button9.Click, AddressOf NumberButton_Click

        ' 演算・イコールボタン
        AddHandler ButtonPlus.Click, AddressOf PlusButton_Click
        AddHandler ButtonEqual.Click, AddressOf EqualButton_Click
    End Sub

    ' 数字入力
    Private Sub NumberButton_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        If resultDisplayed Then
            ' 結果表示後に入力されたらリセット
            input1 = ""
            input2 = ""
            operation = ""
            isSecondInput = False
            resultDisplayed = False
            Label1.Text = ""
            Label2.Text = ""
            LabelOperator.Text = ""
            LabelResult.Text = ""
        End If

        If isSecondInput Then
            input2 &= btn.Text
            Label2.Text = input2
        Else
            input1 &= btn.Text
            Label1.Text = input1
        End If
    End Sub

    ' ＋ボタン処理
    Private Sub PlusButton_Click(sender As Object, e As EventArgs)
        If input1 <> "" And Not isSecondInput Then
            operation = "+"
            isSecondInput = True
            LabelOperator.Text = "+"
        End If
    End Sub

    ' ＝ボタン処理
    Private Sub EqualButton_Click(sender As Object, e As EventArgs)
        If input1 <> "" AndAlso input2 <> "" Then
            Dim result As Double = 0

            Select Case operation
                Case "+"
                    result = Val(input1) + Val(input2)
                    ' 他の演算子も追加可能
            End Select

            ' 結果は右下のラベルに表示
            LabelResult.Text = result.ToString()

            ' 状態更新
            input1 = result.ToString()
            input2 = ""
            operation = ""
            isSecondInput = False
            resultDisplayed = True
        End If
    End Sub
End Class
