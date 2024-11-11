using CrudStoredProcedure.Data;
using CrudStoredProcedure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CrudStoredProcedure.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataAcess _dataAcess;

        public HomeController(DataAcess dataAcess)
        {
            _dataAcess = dataAcess; 
        }

        public IActionResult Index()
        {
            try
            {
                var usuarios = _dataAcess.ListarUsuario();

                return View(usuarios);
            }
            catch (Exception ex) 
            {
                TempData["MensagemErro"] = "Ocorreu um erro na cria��o do usu�rio!";
                return View();
            }

        }

        public IActionResult Cadastrar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            var usuario = _dataAcess.BuscarUsuarioPorId(id);

            return View(usuario);
        }

        public IActionResult Detalhes(int id)
        {
            var usuario = _dataAcess.BuscarUsuarioPorId(id);

            return View(usuario);
        }

        public IActionResult Remover(int id)
        {
            var result = _dataAcess.Remover(id);

            if(result)
            {
                TempData["MensagemSucesso"] = "Usu�rio removido com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro na remo��o do usu�rio!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var result = _dataAcess.CadastrarUsuario(usuario);

                if(result)
                {
                    TempData["MensagemSucesso"] = "Usu�rio criado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro na cria��o do usu�rio!";
                    return View(usuario);
                }
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                var result = _dataAcess.Editar(usuario);
                
                if (result)
                {
                    TempData["MensagemSUcesso"] = "Usu�rio editado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro na edi��o do usu�rio!";
                    return View(usuario);
                }
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro na edi��o do Usu�rio!";
                return View(usuario);
            }
        }


    }
}
