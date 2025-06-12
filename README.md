# calucatest



```vbnet
' 演算子ボタンクリック時の処理
Private Sub OperatorButton_Click(sender As Object, e As EventArgs)
    Dim btn As Button = CType(sender, Button)
    Dim operatorText As String = btn.Text
    Dim currentText As String = TextBoxDisplay.Text

    If currentText.Length > 0 Then
        Dim lastChar As Char = currentText(currentText.Length - 1)

        ' 最後の文字が演算子だったら置き換える
        If "+-*/".Contains(lastChar) Then
            TextBoxDisplay.Text = currentText.Substring(0, currentText.Length - 1) & operatorText
        Else
            TextBoxDisplay.Text &= operatorText
        End If
    End If
End Sub
```

```vbnet
親画面
Public Class MainForm
    Private Sub Button1_DoubleClick(sender As Object, e As EventArgs) _
            Handles Button1.DoubleClick
        ' 子画面を生成
        Using f As New ChildForm()
            ' モーダル表示
            If f.ShowDialog() = DialogResult.OK Then
                ' 子画面から返されたデータを受け取る
                Dim received As String = f.ReturnedData
                MessageBox.Show("子画面から受信: " & received)
                ' ここで親画面のコントロールに反映するなど
                lblResult.Text = received
            Else
                ' キャンセル時の処理（あれば）
            End If
        End Using
    End Sub
End Class


```
