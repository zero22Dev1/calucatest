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