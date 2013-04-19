// -----------------------------------------------------------------------
// <copyright file="Command.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MesserSmash.Commands {


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Command : ICommand {
        private readonly string _name;

        public Command(string name) {
            _name = name;
        }

        public string Name {
            get { return _name; }
        }

        public void execute() {
            custExecute();
            Controller.instance.handleCommand(this);
        }

        protected virtual void custExecute() {}
    }
}
