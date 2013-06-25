using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelChangeTracking.UI
{
    public class ShellViewModel : Screen, IShellViewModel
    {
        public EditableCollection<PersonModel> People { get; set; }

        public int NotChanged { get { return this.People.NotChanged.Count(); } }
        public int Changed { get { return this.People.Changed.Count(); } }
        public int New { get { return this.People.New.Count(); } }
        public int Deleted { get { return this.People.Deleted.Count(); } }

        public ShellViewModel()
        {
            this.People = new EditableCollection<PersonModel>();
            using (UntrackedContext<EditableCollection<PersonModel>>.Untrack(this.People))
            {
                this.People.Add(new PersonModel() { Firstname = "Darko", Lastname = "Micic" });
                this.People.Add(new PersonModel() { Firstname = "Max", Lastname = "Muster" });
                this.People.Add(new PersonModel() { Firstname = "Firstname 1", Lastname = "Lastname 1" });
                this.People.Add(new PersonModel() { Firstname = "Firstname 2", Lastname = "Lastname 2" });
                this.People.Add(new PersonModel() { Firstname = "Firstname 3", Lastname = "Lastname 3" });
                this.People.Add(new PersonModel() { Firstname = "Firstname 4", Lastname = "Lastname 4" });
            }
        }

        public void GetChanges()
        {
            this.NotifyOfPropertyChange(() => this.NotChanged);
            this.NotifyOfPropertyChange(() => this.Changed);
            this.NotifyOfPropertyChange(() => this.New);
            this.NotifyOfPropertyChange(() => this.Deleted);
        }
    }
}
