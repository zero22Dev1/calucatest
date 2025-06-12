# calucatest



```vbnet
子画面
Public Class ChildForm
    ' 親に返却するデータ用プロパティ
    Public Property ReturnedData As String

    Private Sub btnSendToParent_Click(sender As Object, e As EventArgs) _
            Handles btnSendToParent.Click
        ' テキストボックスの値などをプロパティにセット
        Me.ReturnedData = txtInput.Text
        ' OK を返してモーダルを閉じる
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) _
            Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class

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
