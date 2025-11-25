package com.app.myapplication;

import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import org.json.JSONObject;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Scanner;

public class MainActivity extends AppCompatActivity {

    // Lembre-se: No emulador, localhost é 10.0.2.2
    // TROQUE '5234' PELA SUA PORTA HTTP (veja no launchSettings.json)
    private static final String API_URL = "http://10.0.2.2:5204/api/Auth/login";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        EditText edtEmail = findViewById(R.id.edtEmail);
        EditText edtSenha = findViewById(R.id.edtSenha);
        Button btnLogin = findViewById(R.id.btnLogin);

        btnLogin.setOnClickListener(v -> {
            String email = edtEmail.getText().toString();
            String senha = edtSenha.getText().toString();

            if(email.isEmpty() || senha.isEmpty()){
                Toast.makeText(this, "Preencha tudo!", Toast.LENGTH_SHORT).show();
                return;
            }

            // Inicia a conexão em segundo plano
            fazerLogin(email, senha);
        });
    }

    private void fazerLogin(String email, String senha) {
        new Thread(() -> {
            try {
                URL url = new URL(API_URL);
                HttpURLConnection conn = (HttpURLConnection) url.openConnection();
                conn.setRequestMethod("POST");
                conn.setRequestProperty("Content-Type", "application/json");
                conn.setDoOutput(true);

                String jsonInput = "{\"email\": \"" + email + "\", \"senha\": \"" + senha + "\"}";

                try(OutputStream os = conn.getOutputStream()) {
                    byte[] input = jsonInput.getBytes("utf-8");
                    os.write(input, 0, input.length);
                }

                int code = conn.getResponseCode();

                if (code == 200) {
                    // Ler a resposta bruta
                    Scanner scanner = new Scanner(conn.getInputStream());
                    String responseText = scanner.useDelimiter("\\A").next();
                    scanner.close();

                    String tokenFinal = "";

                    try {
                        // TENTATIVA 1: Tenta ler como JSON Objeto {"token":"..."}
                        // Verifique no seu Swagger se o nome do campo é "token", "accessToken" ou outro
                        if (responseText.trim().startsWith("{")) {
                            JSONObject jsonObject = new JSONObject(responseText);

                            // Tenta achar o campo com o token (nomes comuns)
                            if (jsonObject.has("token")) {
                                tokenFinal = jsonObject.getString("token");
                            } else if (jsonObject.has("accessToken")) {
                                tokenFinal = jsonObject.getString("accessToken");
                            } else {
                                // Se não achar o campo, pega o JSON inteiro (caso raro)
                                tokenFinal = responseText;
                            }
                        } else {
                            // TENTATIVA 2: É apenas uma string pura com aspas "ey..."
                            tokenFinal = responseText.replace("\"", "").trim();
                        }

                    } catch (Exception e) {
                        // Se der erro no JSON, assume que é string
                        tokenFinal = responseText.replace("\"", "").trim();
                    }

                    // AQUI ESTÁ O SEGREDINHO: Vamos limpar espaços em branco invisíveis
                    final String tokenLimpo = tokenFinal;

                    runOnUiThread(() -> {
                        // DEBUG: Mostra um pedacinho do token na tela para conferir
                        Toast.makeText(this, "Token: " + tokenLimpo.substring(0, 10) + "...", Toast.LENGTH_SHORT).show();

                        android.content.Intent intent = new android.content.Intent(this, HomeActivity.class);
                        intent.putExtra("TOKEN_ACESSO", tokenLimpo);
                        startActivity(intent);
                    });
                }

            } catch (Exception e) {
                runOnUiThread(() -> Toast.makeText(this, "Erro: " + e.getMessage(), Toast.LENGTH_LONG).show());
            }
        }).start();
    }
}