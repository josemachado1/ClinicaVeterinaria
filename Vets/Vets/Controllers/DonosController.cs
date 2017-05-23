using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vets.Models;

namespace Vets.Controllers
{


    public class DonosController : Controller
    {
        private VetsDB db = new VetsDB();

        // GET: Donos
        public ActionResult Index()
        {
            return View(db.Donos.ToList().OrderBy(d => d.Nome));
        }

        // GET: Donos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // GET: Donos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,NIF")] Donos dono)
        {
            //determinar o nº (ID) a atribuir ao novo DONO
            //criar a var, que recebe esse valor
            int novoID = 0;

            try
            {
                //determinar o novo ID
                //faz a mesma coisa que a de baixo
                /*    novoID = (from d in db.Donos
                              orderby d.DonoID descending
                              select d.DonoID).FirstOrDefault() + 1;
                */
                novoID = db.Donos.Max(d => d.DonoID) + 1;
            }

            catch (SystemException)
            {
                //a tabela 'Donos' está vazia
                //não sendo possivel devolver o MAX de uma tabela
                //por isso, vou atribuir 'manualmente' o valor de 'novoID'
                novoID = 1;
            }
            //atribuir o 'novoID' ao objeto 'dono'
            dono.DonoID = novoID;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Donos.Add(dono);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }            
            catch (Exception)
            {
                //não consigo guardar as alterações 
                //No minimo , preciso de notificar o utilizador que o processo falhou
                ModelState.AddModelError("", "Ocorreu um erro na adição do novo Dono.");
                //Notificar o 'administrador/programador' que ocorreu este erro
                //fazer : 
                //1º enviar mail ao programador a informar da ocorrencia do erro
                //2º ter uma tabela , na BD, onde são reportados os erros:
                // - data
                // - metodo
                // - controller 
                // - detalhes do erro
            }

            return View(dono);
        }

        // GET: Donos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // POST: Donos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonoID,Nome,NIF")] Donos donos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donos);
        }

        // GET: Donos/Delete/5
        public ActionResult Delete(int? id)
        {
            //avalia se o parametro é nulo
            if (id == null)
            {
                return RedirectToAction("Index"); //new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //pesquisar na BD pelo dono cujo ID é fornecido
            Donos dono = db.Donos.Find(id);
            //se o dono não é encontrado...
            if (dono == null)
            {
                //redirecionar para o inicio
                return RedirectToAction("Index"); //HttpNotFound();
            }
            //mostra os dados 'donos'
            return View(dono);
        }

        // POST: Donos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //procurar o 'dono'n, na BD, cuja PK é igual ao parametro fornecido -id-
            Donos dono = db.Donos.Find(id);
            try
            {
            //remove do objeto 'db', o dono encontrado na linha anterior
            db.Donos.Remove(dono);
            //torna defiitivas as instruções anteriores
            db.SaveChanges();

            }
            catch (Exception)
            {
                //gerar uma menssagem de erro a ser entregue ao utilizador
                ModelState.AddModelError("",
                    string.Format("Ocorreu um erro na operação de eliminar o 'dono' com ID {0} - {1}", id, dono.Nome)
                    );
                //regressar à view 'Delete'
                return View(dono);

            }
            //devolve o controlo do programa, apresentando a view 'Index'
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
