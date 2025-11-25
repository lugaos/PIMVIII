package com.app.myapplication.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.app.myapplication.R;
import com.app.myapplication.model.PlaylistModelo;
import com.bumptech.glide.Glide;

import java.util.ArrayList;

public class PlaylistAdapter  extends ArrayAdapter<PlaylistModelo> {

    public PlaylistAdapter(Context context, ArrayList<PlaylistModelo> lista) {
        super(context,0,lista);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        // Verifica se a view já existe, senão cria
        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.item_playlist, parent, false);
        }

        // Pega o item atual
        PlaylistModelo playlist = getItem(position);

        // Acha os componentes da tela
        TextView txtNome = convertView.findViewById(R.id.txtNomePlaylist);
        TextView txtQtd = convertView.findViewById(R.id.txtQtdMusicas);
        TextView txtDur = convertView.findViewById(R.id.txtDuracao);
        ImageView img = convertView.findViewById(R.id.imgCapa);

        // Preenche os textos
        txtNome.setText(playlist.getNome());
        txtQtd.setText(playlist.getQtdMusicas() + " músicas");

        // Preenche os textos
        if (playlist != null) {
            txtNome.setText(playlist.getNome());
            txtQtd.setText(playlist.getQtdMusicas() + " músicas");
            txtDur.setText("3:21");

            // --- LÓGICA DA IMAGEM (GLIDE) ---
            if (playlist.getImagemUrl() != null && !playlist.getImagemUrl().isEmpty()) {
                Glide.with(getContext())
                        .load(playlist.getImagemUrl()) // URL da API
                        .placeholder(android.R.drawable.ic_menu_gallery) // Mostra isso enquanto carrega
                        .error(android.R.drawable.ic_delete) // Mostra isso se o link estiver quebrado
                        .centerCrop() // Ajusta a imagem para não distorcer
                        .into(img); // Coloca na ImageView
            } else {
                // Se não tiver link, coloca uma imagem padrão
                img.setImageResource(android.R.drawable.ic_menu_gallery);
            }
        }

        return convertView;
    }
}
