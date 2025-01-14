namespace WebApi.Services;

using AutoMapper;
using BCrypt.Net;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Scores;

public interface IScoreService
{
    IEnumerable<Score> GetAll();
    Score GetById(int id);
    IEnumerable<Score> GetByCourseId(int courseid);
    void Create(CreateRequest model);
    //void Update(int id, UpdateRequest model);
    void Delete(int id);
}

public class ScoreService : IScoreService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public ScoreService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<Score> GetAll()
    {
        return _context.score;
    }

    public Score GetById(int id)
    {
        return getScore(id);
    }

    public IEnumerable<Score> GetByCourseId(int courseid)
    {
        return _context.score.Where(x=>x.Courseid == courseid);
    }

    public void Create(CreateRequest model)
    {
        // map model to new score object
        var score = _mapper.Map<Score>(model);

        // save score
        _context.score.Add(score);
        _context.SaveChanges();
    }

    //public void Update(int id, UpdateRequest model)
    //{
    //    var score = getScore(id);

    //    // validate
    //    if (model.Email != score.Email && _context.Scores.Any(x => x.Email == model.Email))
    //        throw new AppException("Score with the email '" + model.Email + "' already exists");

    //    // hash password if it was entered
    //    if (!string.IsNullOrEmpty(model.Password))
    //        score.PasswordHash = BCrypt.HashPassword(model.Password);

    //    // copy model to score and save
    //    _mapper.Map(model, score);
    //    _context.Scores.Update(score);
    //    _context.SaveChanges();
    //}

    public void Delete(int id)
    {
        var score = getScore(id);
        _context.score.Remove(score);
        _context.SaveChanges();
    }

    // helper methods

    private Score getScore(int id)
    {
        var score = _context.score.Find(id);
        if (score == null) throw new KeyNotFoundException("Score not found");
        return score;
    }
}