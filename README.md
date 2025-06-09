# calucatest



```vbnet

For col As Integer = 0 To C1FlexGrid1.Cols.Count - 1
    C1FlexGrid1.SetCellStyle(2, col, CreateBoldStyle(C1FlexGrid1, 16)) ' 2行目
Next

```

```vbnet

' たとえば、フォームのロード時などに設定
Dim bigFont As New Font("Meiryo", 14, FontStyle.Bold)


'全体的に
' 本体（セル）のフォントを設定
C1FlexGrid1.Font = bigFont

' ヘッダー（固定行・固定列）のフォントも明示的に設定
C1FlexGrid1.Styles.Fixed.Font = bigFont

' 行ヘッダーの高さや列幅も必要に応じて調整
C1FlexGrid1.Rows.DefaultSize = 30   ' 行の高さ
C1FlexGrid1.Cols.DefaultSize = 100  ' 列の幅（必要に応じて）

' 自動調整（内容に合わせて列幅変更したい場合）
C1FlexGrid1.AutoSizeCols()

```
