using Amazon.Runtime;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

namespace ImageManagerApp.Controllers
{
    public class BucketController : Controller
    {
        private readonly IAmazonS3 _s3Client;
        public BucketController()
        {
            var credentials = new BasicAWSCredentials("AKIAXVYKHZT4JY5YR3OQ", "evSoJosRddBWib0j4OieCqTGjowMka1bnIfADAIT");
            _s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
        }

        [HttpGet("get-all-buckets")]
        public async Task<IActionResult> GetAllBucketAsync()
        {
            var data = await _s3Client.ListBucketsAsync();
            var buckets = data.Buckets.Select(b => { return b.BucketName; });
            return Ok(buckets);
        }

        [HttpPost("create-buckets")]
        public async Task<IActionResult> CreateBucketAsync(string bucketName)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (bucketExists) return BadRequest($"Bucket {bucketName} already exists.");
            await _s3Client.PutBucketAsync(bucketName);
            return Ok($"Bucket {bucketName} created.");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBucketAsync(string bucketName)
        {
            await _s3Client.DeleteBucketAsync(bucketName);
            return NoContent();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
