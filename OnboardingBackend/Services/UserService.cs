
#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnboardingBackend.Models;


namespace OnboardingBackend.Services
{

    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IMongoClient dbClient)
        {
            var db = dbClient.GetDatabase("onboarding");
            
            _userCollection = db.GetCollection<User>("onboardingUsers");
        }

      
        public async Task<List<User>> GetAll()=>
        await _userCollection.Find(_ => true).ToListAsync();
        public async Task<User?> GetAsync(string id) =>
            await _userCollection.Find(x => x.UserId == id).FirstOrDefaultAsync();
           

        public async Task CreateAsync(User newUser) =>
            await _userCollection.InsertOneAsync(newUser);
        public async Task UpdateAsync(string id, User updatedUser) =>
            await _userCollection.ReplaceOneAsync(x => x.UserId == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _userCollection.DeleteOneAsync(x => x.UserId == id);
        
        public async Task<User?> UpdateOneAsync(string id, IceContactModel iceForm)

        {
            var update = Builders<User>.Update.Set("IceContacts", iceForm);
            await _userCollection.UpdateOneAsync(x => x.UserId == id,  update);
            return await _userCollection.Find(x => x.UserId == id).FirstOrDefaultAsync();
        }
        
        public async Task<User?> UpdateHardwareAsync(string id,  HardwareFormModel hardwareForm)
        {
            var update = Builders<User>.Update.Set("RequestedHardware", hardwareForm);
            _userCollection.UpdateOne(x => x.UserId == id,  update);
            return await _userCollection.Find(x => x.UserId == id).FirstOrDefaultAsync();
        }
        
        public async Task<User?> UpdatePersonalDetailsAsync(string id,  PersonalDetailsModel personalDetails)
        {
            var update = Builders<User>.Update.Set("Data", personalDetails);
            _userCollection.UpdateOne(x => x.UserId == id,  update);
            return await _userCollection.Find(x => x.UserId == id).FirstOrDefaultAsync();
        }
        public async Task<User?> UpdateGithubAsync(string id,  Github github)
        {
            var update = Builders<User>.Update.Set("GithubUsername", github.Username);
            _userCollection.UpdateOne(x => x.UserId == id,  update);
            return await _userCollection.Find(x => x.UserId == id).FirstOrDefaultAsync();
        }
        public async Task<User?> UpdateStepsAsync(string id,  Step stepName)
        {
            var user = await _userCollection.Find(x => x.UserId == id).FirstOrDefaultAsync();
            var index = user.CompletedSteps.FindIndex(step => step.StepName == stepName.StepName);
            var update = Builders<User>.Update.Set($"CompletedSteps.{index}.Completed", stepName.Completed);
            _userCollection.UpdateOne(x => x.UserId == id,  update);
            return await _userCollection.Find(x => x.UserId == id).FirstOrDefaultAsync();
        }
    }

    
}
