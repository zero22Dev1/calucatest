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
Dim input As String = "6×6＝36" ' 「×」は U+00D7 など
input = input.Replace("×", "*") ' U+00D7
input = input.Replace("✖︎", "*") ' U+2716 + variation
input = input.Replace("✕", "*") ' U+2715
```
