```vbnet
Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ' 画面の中央位置を取得
    Dim screenBounds As Rectangle = Screen.PrimaryScreen.WorkingArea
    Dim centerX As Integer = (screenBounds.Width - Me.Width) \ 2
    Dim centerY As Integer = (screenBounds.Height - Me.Height) \ 2

    ' 少し上（例：50ピクセル上）にずらす
    Dim offsetY As Integer = 50

    ' 位置を設定
    Me.StartPosition = FormStartPosition.Manual
    Me.Location = New Point(centerX, Math.Max(0, centerY - offsetY))
End Sub
```



```vbnet
' ロック用スタイルの作成（最初に一度だけ）
If C1FlexGrid1.Styles("Locked") Is Nothing Then
    Dim lockStyle = C1FlexGrid1.Styles.Add("Locked")
    lockStyle.Locked = True
    lockStyle.BackColor = Color.LightGray ' 見た目でロックを表現したい場合
End If

' チェックボックス列のインデックスを取得
Dim chkCol As Integer = C1FlexGrid1.Cols("チェック").Index

' 条件に応じてロック
For row As Integer = C1FlexGrid1.Rows.Fixed To C1FlexGrid1.Rows.Count - 1
    ' 例：「状態」列が「完了」の場合にチェックボックスをロック
    If C1FlexGrid1(row, "状態").ToString() = "完了" Then
        C1FlexGrid1(row, chkCol) = C1FlexGrid1(row, chkCol) ' 値そのまま
        C1FlexGrid1.SetCellStyle(row, chkCol, C1FlexGrid1.Styles("Locked"))
    End If
Next
```


```vbnet
Private Sub SelectRowByRecnoOrTop(recno As String)
    Dim dataStartRow As Integer = C1FlexGrid1.Rows.Fixed ' 通常2行のヘッダー想定
    Dim targetRowIndex As Integer = -1

    If recno = "0" Then
        targetRowIndex = dataStartRow
    Else
        Dim colRecno As Integer = C1FlexGrid1.Cols("recno").Index

        ' データ行から検索
        For row As Integer = dataStartRow To C1FlexGrid1.Rows.Count - 1
            If C1FlexGrid1(row, colRecno).ToString() = recno Then
                targetRowIndex = row
                Exit For
            End If
        Next

        ' 見つからなければ先頭データ行を選択
        If targetRowIndex = -1 Then
            targetRowIndex = dataStartRow
        End If
    End If

    ' 対象行を選択
    If targetRowIndex < C1FlexGrid1.Rows.Count Then
        C1FlexGrid1.Select(targetRowIndex, 0)
        C1FlexGrid1.Row = targetRowIndex
    End If
End Sub
```



```vbnet
Private Function GetColumnTotal(columnName As String) As Decimal
    Dim total As Decimal = 0
    Dim colIndex As Integer = C1FlexGrid1.Cols(columnName).Index

    For row As Integer = C1FlexGrid1.Rows.Fixed To C1FlexGrid1.Rows.Count - 1
        Dim cellValue = C1FlexGrid1(row, colIndex)
        If Not IsDBNull(cellValue) AndAlso IsNumeric(cellValue) Then
            total += Convert.ToDecimal(cellValue)
        End If
    Next

    Return total
End Function
```


```vbnet
For row As Integer = C1FlexGrid1.Rows.Fixed To C1FlexGrid1.Rows.Count - 1
    If C1FlexGrid1(row, "状態").ToString() = "完了" Then
        C1FlexGrid1.SetCellStyle(row, C1FlexGrid1.Cols("チェック").Index, C1FlexGrid1.Styles("Locked"))
        C1FlexGrid1(row, C1FlexGrid1.Cols("チェック").Index, True) = False ' 編集不可
    End If
Next
```



```vbnet
' 例：フォームのLoadイベントなどで実行
Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ' サンプルデータの追加
    C1FlexGrid1.Rows.Count = 5
    C1FlexGrid1.Cols.Count = 4
    C1FlexGrid1.Rows.Fixed = 2  ' ヘッダー行を2行に

    ' 列名（Name）を設定
    C1FlexGrid1.Cols(1).Name = "ProductName"
    C1FlexGrid1.Cols(2).Name = "Amount"
    C1FlexGrid1.Cols(3).Name = "Quantity"

    ' ヘッダーキャプション設定（2行分）
    C1FlexGrid1(0, 1) = "商品情報"
    C1FlexGrid1(0, 2) = "価格情報"
    C1FlexGrid1(0, 3) = "数量情報"

    C1FlexGrid1(1, 1) = "商品名"
    C1FlexGrid1(1, 2) = "金額"
    C1FlexGrid1(1, 3) = "数量"

    ' スタイル作成
    Dim amtHeaderStyle = C1FlexGrid1.Styles.Add("AmountHeaderStyle")
    amtHeaderStyle.BackColor = Color.LightBlue
    amtHeaderStyle.ForeColor = Color.Black
    amtHeaderStyle.Font = New Font("Meiryo", 9, FontStyle.Bold)

    ' 列名を使ってインデックスを取得し、ヘッダー2行にスタイルを適用
    Dim amtColIdx As Integer = C1FlexGrid1.Cols("Amount").Index
    For row As Integer = 0 To 1
        C1FlexGrid1.SetCellStyle(row, amtColIdx, amtHeaderStyle)
    Next
End Sub
```

```vb.net
' 処理対象列の「Name」をリストで定義
Dim targetColNames As String() = {"金額", "調整額", "割引額"}

' 赤文字スタイル（なければ作成）
Dim redTextStyle = C1FlexGrid1.Styles("NegativeValueTextStyle")
If redTextStyle Is Nothing Then
    redTextStyle = C1FlexGrid1.Styles.Add("NegativeValueTextStyle")
    redTextStyle.ForeColor = Color.Red
End If

' 全列を確認し、Nameが一致する列インデックスを収集
Dim targetColIndices As New List(Of Integer)
For col As Integer = C1FlexGrid1.Cols.Fixed To C1FlexGrid1.Cols.Count - 1
    If targetColNames.Contains(C1FlexGrid1.Cols(col).Name) Then
        targetColIndices.Add(col)
    End If
Next

' データ行ごとに処理
For row As Integer = C1FlexGrid1.Rows.Fixed To C1FlexGrid1.Rows.Count - 1
    For Each col In targetColIndices
        Dim value As Double
        If Double.TryParse(C1FlexGrid1(row, col).ToString(), value) Then
            If value < 0 Then
                C1FlexGrid1(row, col) = Math.Abs(value)
                C1FlexGrid1.SetCellStyle(row, col, redTextStyle)
            End If
        End If
    Next
Next
```
