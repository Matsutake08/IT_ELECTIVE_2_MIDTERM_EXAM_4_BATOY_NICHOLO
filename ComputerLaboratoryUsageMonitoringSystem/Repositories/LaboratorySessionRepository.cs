using ComputerLaboratoryUsageMonitoringSystem.Models;

namespace ComputerLaboratoryUsageMonitoringSystem.Repositories;

public class LaboratorySessionRepository
{
    private static readonly List<LaboratorySession> Sessions = new();

    public List<LaboratorySession> GetAll(string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return Sessions;
        }

        search = search.ToLower();
        return Sessions.Where(session =>
            session.StudentNumber.ToLower().Contains(search) ||
            session.FirstName.ToLower().Contains(search) ||
            session.LastName.ToLower().Contains(search) ||
            session.ComputerNumber.ToLower().Contains(search) ||
            session.Status.ToLower().Contains(search)).ToList();
    }

    public LaboratorySession? GetById(int id)
    {
        return Sessions.FirstOrDefault(session => session.Id == id);
    }

    public void Add(LaboratorySession session)
    {
        session.Id = Sessions.Count == 0 ? 1 : Sessions.Max(item => item.Id) + 1;
        session.SessionNumber = $"LAB-{session.Id:0000}";
        session.TimeIn = DateTime.Now;
        session.Status = "Using";
        Sessions.Add(session);
    }

    public void Update(LaboratorySession updatedSession)
    {
        LaboratorySession? session = GetById(updatedSession.Id);
        if (session == null)
        {
            return;
        }

        session.StudentNumber = updatedSession.StudentNumber;
        session.FirstName = updatedSession.FirstName;
        session.LastName = updatedSession.LastName;
        session.Course = updatedSession.Course;
        session.YearLevel = updatedSession.YearLevel;
        session.ComputerNumber = updatedSession.ComputerNumber;
        session.Purpose = updatedSession.Purpose;
        session.Notes = updatedSession.Notes;
    }

    public void RecordTimeOut(int id)
    {
        LaboratorySession? session = GetById(id);
        if (session != null)
        {
            session.TimeOut = DateTime.Now;
            session.Status = "Finished";
        }
    }
}
