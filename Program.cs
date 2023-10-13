using Microsoft.Azure.Cosmos;




public class Program
{

    // The Cosmos client instance
    private CosmosClient cosmosClient;

    // The database we will create
    private Database database;

    // The container we will create.
    private Container container;

    // The names of the database and container we will create
    private string databaseId = "az204Database";
    private string containerId = "az204Container";



    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Beginning operations...\n");
            Program p = new Program();
            await p.CosmosAsync();

        }
        catch (CosmosException de)
        {
            Exception baseException = de.GetBaseException();
            Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }
        finally
        {
            Console.WriteLine("End of program, press any key to exit.");
            Console.ReadKey();
        }
    }
    //The sample code below gets added below this line


    public async Task CosmosAsync()
    {


        var cosmosOptions = new CosmosClientOptions
        {
            HttpClientFactory = () => new HttpClient(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            }),
            ConnectionMode = ConnectionMode.Gateway,
            LimitToEndpoint = true
        };


        // Create a new instance of the Cosmos Client
        this.cosmosClient = new CosmosClient("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", cosmosOptions);

        // Runs the CreateDatabaseAsync method
        await CreateDatabaseAsync();

        // Run the CreateContainerAsync method
        await CreateContainerAsync();
    }



    private async Task CreateDatabaseAsync()
    {
        // Create a new database
        this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        Console.WriteLine("Created Database: {0}\n", this.database.Id);
    }


    private async Task CreateContainerAsync()
    {
        // Create a new container
        this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
        Console.WriteLine("Created Container: {0}\n", this.container.Id);
    }

}