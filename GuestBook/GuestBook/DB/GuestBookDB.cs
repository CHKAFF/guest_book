using GuestBook.Models;
using GuestBook.Models.Requests;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace GuestBook.DB;

public class GuestBookDB
{
    private TableClient client;
    
    public GuestBookDB(TableClient client)
    {
        this.client = client;
    }

    public async Task<Guest[]> GetAll()
    {
        var response = await client.SessionExec(async session =>
        {
            var query = @"SELECT id, Name, Message, AddedDate FROM Guests";

            return await session.ExecuteDataQuery(
                query: query,
                parameters: new Dictionary<string, YdbValue>(),
                txControl: TxControl.BeginSerializableRW().Commit()
            );
        });

        response.Status.EnsureSuccess();
        var queryResponse = (ExecuteDataQueryResponse)response;
        return queryResponse.Result.ResultSets[0].Rows.Select(row => new Guest
            {
                Id = (ulong?)row["id"],
                Name = (string?)row["Name"],
                Message = (string?)row["Message"],
                AddedDate = (DateTime?)row["AddedDate"]
            }).ToArray();
    }

    public async Task<Guest> Create(CreateGuestRequest createGuestRequest)
    {
        var newId = (await GetAll()).Max(g => g.Id).Value + 1;
        var addedDate = DateTime.Now;
        
        var response = await client.SessionExec(async session =>
        {
            var query = @"
DECLARE $id AS Uint64;
DECLARE $name AS Utf8;
DECLARE $message AS Utf8;
DECLARE $addeddate AS Datetime;

INSERT INTO Guests (id, AddedDate, Name, Message) VALUES ($id, $addeddate, $name, $message)";

            return await session.ExecuteDataQuery(
                query: query,
                parameters: new Dictionary<string, YdbValue>()
                {
                    {"$id", YdbValue.MakeUint64(newId)},
                    {"$name", YdbValue.MakeUtf8(createGuestRequest.Name)},
                    {"$message", YdbValue.MakeUtf8(createGuestRequest.Message)},
                    {"$addeddate", YdbValue.MakeDatetime(addedDate)},
                },
                txControl: TxControl.BeginSerializableRW().Commit()
            );
        });

        response.Status.EnsureSuccess();
        
        return new Guest()
        {
            Id = newId,
            Name = createGuestRequest.Name,
            Message = createGuestRequest.Message,
            AddedDate = addedDate
        };
    }
}