package com.app.myapplication;// Mantenha seu pacote

import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.app.myapplication.adapter.PlaylistAdapter;
import com.app.myapplication.model.PlaylistModelo;
import com.bumptech.glide.Glide;

import org.json.JSONArray;
import org.json.JSONObject;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.Scanner;

public class HomeActivity extends AppCompatActivity {

    // ⚠️ USE A MESMA PORTA HTTP DO LOGIN
    private static final String API_PLAYLIST = "http://10.0.2.2:5204/api/Playlist";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);

        ListView listView = findViewById(R.id.listaPlaylists);

        // Recuperar o token que veio da tela de Login
        String token = getIntent().getStringExtra("TOKEN_ACESSO");

        if (token != null) {
            carregarPlaylists(token, listView);
        }

        ListView lista = findViewById(R.id.listaPlaylists);
        TextView txtTitulo = findViewById(R.id.txtPlayerTitulo);
        TextView txtArtista = findViewById(R.id.txtPlayerArtista);
        ImageView btnPlay = findViewById(R.id.btnPlayPause);

        ImageView imgPlayerCapa = findViewById(R.id.imgPlayerCapa); // <--- Certifique-se de ter essa linha

        lista.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                PlaylistModelo playlistClicada = (PlaylistModelo) parent.getItemAtPosition(position);

                if (playlistClicada != null) {
                    txtTitulo.setText(playlistClicada.getNome());
                    txtArtista.setText("Tocando agora");

                    Glide.with(HomeActivity.this)
                            .load(playlistClicada.getImagemUrl()) // <--- Troque pelo método que pega a URL na sua Model
                            .placeholder(R.drawable.back_wall3)   // Imagem que mostra enquanto carrega
                            .into(imgPlayerCapa);

                    btnPlay.setImageResource(R.drawable.baseline_pause_24);
                }
            }
        });

    }


    private void carregarPlaylists(String token, ListView listView) {
        new Thread(() -> {
            try {
                URL url = new URL(API_PLAYLIST);
                HttpURLConnection conn = (HttpURLConnection) url.openConnection();
                conn.setRequestMethod("GET");

                // O segredo: Enviar o Token no cabeçalho
                conn.setRequestProperty("Authorization", "Bearer " + token);

                int code = conn.getResponseCode();

                if (code == 200) {
                    // Ler o JSON de resposta
                    Scanner scanner = new Scanner(conn.getInputStream());
                    String jsonResponse = scanner.useDelimiter("\\A").next();
                    scanner.close();

                    // Processar o JSON (Parse)
                    JSONArray array = new JSONArray(jsonResponse);

                    ArrayList<PlaylistModelo> listaPlaylists = new ArrayList<>();

                    for (int i = 0; i < array.length(); i++) {
                        JSONObject obj = array.getJSONObject(i);

                        String nome = obj.getString("nome");
                        // Pega a URL (trata caso venha nulo)
                        String img = obj.has("imagemUrl") ? obj.getString("imagemUrl") : "";
                        // Pega a quantidade (que o backend calculou)
                        int qtd = obj.has("quantidadeMusicas") ? obj.getInt("quantidadeMusicas") : 0;

                        listaPlaylists.add(new PlaylistModelo(nome, img, qtd));
                    }

                    runOnUiThread(() -> {
                        // Usa o NOSSO adapter personalizado
                        PlaylistAdapter adapter = new PlaylistAdapter(HomeActivity.this, listaPlaylists);
                        listView.setAdapter(adapter);
                    });
                } else {
                    runOnUiThread(() -> Toast.makeText(this, "Erro ao buscar: " + code, Toast.LENGTH_LONG).show());
                }

            } catch (Exception e) {
                e.printStackTrace();
            }
        }).start();
    }
}