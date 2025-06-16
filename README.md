# calucatest


```sql
CREATE OR REPLACE PACKAGE pkg_update_flag IS
  -- IDと更新時間の配列型の定義
  TYPE id_array IS TABLE OF NUMBER INDEX BY BINARY_INTEGER;
  TYPE updtime_array IS TABLE OF DATE INDEX BY BINARY_INTEGER;

  -- フラグ更新プロシージャ
  PROCEDURE update_flag(
    p_ids        IN id_array,
    p_upd_times  IN updtime_array,
    o_result     OUT NUMBER,
    o_err_msg    OUT VARCHAR2
  );
END pkg_update_flag;
/
```





















```sql
CREATE OR REPLACE PACKAGE BODY pkg_update_flag IS

  PROCEDURE update_flag(
    p_ids        IN id_array,
    p_upd_times  IN updtime_array,
    o_result     OUT NUMBER,
    o_err_msg    OUT VARCHAR2
  ) IS
    v_dummy         NUMBER;
    v_error_count   PLS_INTEGER := 0;
  BEGIN
    o_err_msg := NULL;

    FOR i IN p_ids.FIRST .. p_ids.LAST LOOP
      BEGIN
        -- 1. 排他ロックを取得
        SELECT 1
        INTO v_dummy
        FROM your_table
        WHERE id = p_ids(i)
          AND update_time = p_upd_times(i)
        FOR UPDATE NOWAIT;

        -- 2. フラグ更新
        UPDATE your_table
        SET flag = 'Y'
        WHERE id = p_ids(i);

        IF SQL%ROWCOUNT = 0 THEN
          v_error_count := v_error_count + 1;
          o_result := 9;
          o_err_msg := '合計 ' || TO_CHAR(v_error_count) || ' 件のエラー: ID=' || p_ids(i) ||
                       ' のフラグ更新に失敗（対象行が存在しない可能性）';
          RETURN;
        END IF;

      EXCEPTION
        WHEN OTHERS THEN
          v_error_count := v_error_count + 1;
          o_result := 9;
          o_err_msg := '合計 ' || TO_CHAR(v_error_count) || ' 件のエラー: ID=' || p_ids(i) ||
                       ' で例外が発生：' || SQLERRM;
          RETURN;
      END;
    END LOOP;

    -- すべて成功
    COMMIT;
    o_result := 0;
    o_err_msg := NULL;

  END update_flag;

END pkg_update_flag;
/
```
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
