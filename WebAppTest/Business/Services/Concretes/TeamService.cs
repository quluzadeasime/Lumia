using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IWebHostEnvironment _environment;

        public TeamService(IWebHostEnvironment environment, ITeamRepository teamRepository = null)
        {
            _environment = environment;
            _teamRepository = teamRepository;
        }

        public void Add(Team team)
        {
            if (team == null) throw new TeamNullException("", "Team null ola blmez");
            if (!team.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "Content type sehvdir");
            if (team.PhotoFile.Length > 2097152) throw new FileSizeException("", "Max olcu 2 mb ola biler");

            string path = _environment.WebRootPath + @"\uploads\" + team.PhotoFile.FileName;
            using(FileStream file = new FileStream(path, FileMode.Create))
            {
                team.PhotoFile.CopyTo(file);
            }


            team.ImgUrl = team.PhotoFile.FileName;
            _teamRepository.Add(team);
            _teamRepository.Commit();

        }

        public void Delete(int id)
        {
            var existteam = _teamRepository.Get(x=>x.Id == id);
            if (existteam == null) throw new TeamNullException("", "Team yoxdur!");

            string path = _environment.WebRootPath + @"\uploads\" + existteam.ImgUrl;

            if (!File.Exists(path)) throw new Exceptions.FileNotFoundException("", "File yoxdur");

            File.Delete(path);
            _teamRepository.Delete(existteam);
            _teamRepository.Commit();
        }

        public Team Get(Func<Team, bool> func = null)
        {
            return _teamRepository.Get(func);
        }

        public List<Team> GetAll(Func<Team, bool> func = null)
        {
            return _teamRepository.GetAll(func);
        }

        public void Update(int id, Team team)
        {
            var existteam = _teamRepository.Get(x => x.Id == id);
            if (existteam == null) throw new TeamNullException("", "Team yoxdur!");
            if (team == null) throw new TeamNullException("", "Team yoxdur");

            if(team.PhotoFile != null)
            {
                if (!team.PhotoFile.ContentType.Contains("image/")) throw new ContentTypeException("PhotoFile", "Content type sehvdir");
                if (team.PhotoFile.Length > 2097152) throw new FileSizeException("", "Max olcu 2 mb ola biler");

                string path = _environment.WebRootPath + @"\uploads\" + team.PhotoFile.FileName;
                using (FileStream file = new FileStream(path, FileMode.Create))
                {
                    team.PhotoFile.CopyTo(file);
                }


                team.ImgUrl = team.PhotoFile.FileName;
            }
            existteam.Fullname= team.Fullname;
            existteam.Position = team.Position;
            existteam.Description= team.Description;
            _teamRepository.Commit();
        }
    }
}
