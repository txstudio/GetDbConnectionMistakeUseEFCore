# 在 EntityFrameworkCore 使用 Database.GetDbConnection() 的錯誤範例

在網路上文章會看到呼叫 Database.GetDbConnection() 方法操作連線字串時會看到類似下列的撰寫方式

```
using(var _conn = dbContext.Database.GetDbConnection())
{
  //使用 _conn 的資料庫連線物件進行資料表操作
}
```

使用此方法呼叫後，dbContext 的資料庫連線物件就會被關閉

若之後要重新取得 dbContext.Entity 物件的話就會出現錯誤訊息

```
System.InvalidOperationException: 'The ConnectionString property has not been initialized.'
```

若要取得資料庫連線物件時，再有使用 using 片段操作 DbContext 物件時，不需要使用 using 來釋放 DbConnection 物件

> 更多詳細資料程式碼片段範例請參考 Program.cs 程式碼註解內容

## 範例程式碼說明

- 使用 EntityFrameworkCore 搭配 .NET Core ConsoleApp
- 預設資料庫使用 Microsoft SQL Server
- 使用 Code First 建立資料庫環境
